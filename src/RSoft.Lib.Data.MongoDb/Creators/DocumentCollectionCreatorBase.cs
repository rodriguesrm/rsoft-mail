using MongoDB.Bson;
using MongoDB.Driver;
using RSoft.Lib.Data.MongoDb.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RSoft.Lib.Data.MongoDb.Creators
{

    /// <summary>
    /// Document collection creator abstract class
    /// </summary>
    /// <typeparam name="TDocument">Document type</typeparam>
    public abstract class DocumentCollectionCreatorBase<TDocument>
        where TDocument : IDocument
    {

        #region Local/Objects variables

        protected string _collectionName;
        protected IMongoCollection<TDocument> _collection;
        protected IMongoDatabase _mongoDatabase;

        /// <summary>
        /// Database collection
        /// </summary>
        protected IMongoCollection<TDocument> Collection
        {
            get
            {
                _collection ??= _mongoDatabase.GetCollection<TDocument>(_collectionName);
                return _collection;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new class instance
        /// </summary>
        /// <param name="mongoDatabase">IMongoDatabase object instance</param>
        public DocumentCollectionCreatorBase(IMongoDatabase mongoDatabase, string collectionName)
        {
            _collectionName = collectionName;
            _mongoDatabase = mongoDatabase;
        }

        #endregion

        #region Public/Abstract methods

        ///<inheritdoc/>
        public abstract Task CreateCollection();

        #endregion

        #region ICollectionCreator

        /// <summary>
        /// Check if collection exists
        /// </summary>
        protected async Task<bool> IsCollectionExists()
        {

            IAsyncCursor<BsonDocument> collections = await _mongoDatabase.ListCollectionsAsync();
            bool result = false;

            if (collections != null)
                await collections.ForEachAsync(document => result = result || document["name"].AsString.Equals(_collectionName));

            return result;

        }

        /// <summary>
        /// Create collection if not exists
        /// </summary>
        protected async Task<IMongoCollection<TDocument>> CreateCollectionAsync()
            => await CreateCollectionAsync(false, null, null);

        /// <summary>
        /// Create collection if not exists
        /// </summary>
        /// <param name="isCapped">Indicates whether the collection will be capped type</param>
        /// <param name="maxSize">Maximum size of the collection (used with capped collections)</param>
        /// <param name="maxDocuments">Maximum number of documents (used with capped collections)</param>
        protected async Task<IMongoCollection<TDocument>> CreateCollectionAsync(bool isCapped, long? maxSize, long? maxDocuments)
        {

            if (!(await IsCollectionExists()))
            {

                if (isCapped && maxSize > 0)
                {

                    CreateCollectionOptions options = new CreateCollectionOptions
                    {
                        Capped = isCapped,
                        MaxSize = maxSize
                    };

                    if (maxDocuments > 0)
                        options.MaxDocuments = maxDocuments;

                    await _mongoDatabase.CreateCollectionAsync(_collectionName, options);

                }
                else
                {
                    await _mongoDatabase.CreateCollectionAsync(_collectionName);
                }

            }

            return Collection;

        }


        /// <summary>
        /// Check if index exists
        /// </summary>
        /// <param name="collection">Collection object instance</param>
        /// <param name="indexNames">List of index names to be checked</param>
        protected async Task<bool> IsIndexExists(IMongoCollection<TDocument> collection, params string[] indexNames)
        {
            bool result = false;
            using (IAsyncCursor<BsonDocument> cursor = await collection.Indexes.ListAsync())
            {
                IEnumerable<BsonDocument> existingIndexes = await cursor.ToListAsync();
                foreach (BsonDocument index in existingIndexes)
                {
                    BsonDocument currentIndex = index.GetValue("key", null).ToBsonDocument();
                    if (currentIndex == null) continue;
                    if (indexNames.Any(checkIndex => (currentIndex.GetValue(checkIndex, 0).ToBoolean())))
                        result = true;
                }
            }

            return result;

        }

        /// <summary>
        /// Create index if not exists
        /// </summary>
        /// <param name="field">Field object</param>
        /// <param name="indexName">Index name</param>
        protected Task CreateIndexAsync(Expression<Func<TDocument, object>> field, string indexName)
            => CreateIndexAsync(field, indexName, false, null);

        /// <summary>
        /// Create index if not exists
        /// </summary>
        /// <param name="field">Field object</param>
        /// <param name="indexName">Index name</param>
        /// <param name="isUnique">Indicates whether the index is unique/exclusive</param>
        protected Task CreateIndexAsync(Expression<Func<TDocument, object>> field, string indexName, bool isUnique)
        {
            return CreateIndexAsync(field, indexName, isUnique, null);
        }

        /// <summary>
        /// Create index if not exists
        /// </summary>
        /// <param name="indexName">Index name</param>
        /// <param name="isUnique">Indicates whether the index is unique/exclusive</param>
        /// <param name="lifetime">Index lifetime</param>
        /// <param name="fields">Fields list</param>
        protected async Task CreateIndexAsync(string indexName, bool isUnique, int? lifetime, params Expression<Func<TDocument, object>>[] fields)
        {

            if (Collection != null)
            {
                if (!await IsIndexExists(Collection, indexName))
                {
                    IndexKeysDefinition<TDocument>[] indexKeys = new IndexKeysDefinition<TDocument>[fields.Length];

                    for (int position = 0; position < fields.Length; position++)
                    {
                        indexKeys[position] = Builders<TDocument>.IndexKeys.Ascending(fields[position]);
                    }

                    CreateIndexOptions indexParameters;

                    if (lifetime.HasValue)
                        indexParameters = new CreateIndexOptions { Name = indexName, Unique = isUnique, ExpireAfter = TimeSpan.FromDays(lifetime.Value) };
                    else
                        indexParameters = new CreateIndexOptions { Name = indexName, Unique = isUnique };

                    IndexKeysDefinition<TDocument> indexDefinition = Builders<TDocument>.IndexKeys.Combine(indexKeys);
                    CreateIndexModel<TDocument> indexModel = new CreateIndexModel<TDocument>(indexDefinition, indexParameters);
                    await Collection.Indexes.CreateOneAsync(indexModel);

                }
            }
        }

        /// Create index if not exists
        /// </summary>
        /// <param name="field">Field object</param>
        /// <param name="indexName">Index name</param>
        /// <param name="isUnique">Indicates whether the index is unique/exclusive</param>
        /// <param name="lifetime">Index lifetime</param>
        protected async Task CreateIndexAsync(Expression<Func<TDocument, object>> field, string indexName, bool isUnique, int? lifetime)
        {

            if (Collection != null)
            {
                if (!await IsIndexExists(Collection, indexName))
                {

                    TimeSpan? days = null;
                    if (lifetime.HasValue)
                        days = TimeSpan.FromDays(lifetime.Value);

                    CreateIndexOptions indexParameters = new CreateIndexOptions { Name = indexName, Unique = isUnique, ExpireAfter = days };
                    CreateIndexModel<TDocument> indexModel = new CreateIndexModel<TDocument>(Builders<TDocument>.IndexKeys.Ascending(field), indexParameters);
                    await Collection.Indexes.CreateOneAsync(indexModel);

                }
            }
        }

        #endregion

    }
}
