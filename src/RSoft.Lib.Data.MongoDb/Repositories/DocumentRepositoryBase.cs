using MongoDB.Driver;
using RSoft.Lib.Data.MongoDb.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSoft.Lib.Data.MongoDb.Repositories
{
    public abstract class DocumentRepositoryBase<TDocument> : IDocumentRepository<TDocument>
        where TDocument : IDocument
    {

        #region Local Objects/Variables

        protected readonly string _collectionName;
        protected readonly IMongoDatabase _db;

        /// <summary>
        /// Document collection from database
        /// </summary>
        protected IMongoCollection<TDocument> Collection => _db.GetCollection<TDocument>(_collectionName);

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of document repository
        /// </summary>
        /// <param name="db">MongoDatabase connection instance</param>
        /// <param name="collectionName">Collection name</param>
        public DocumentRepositoryBase(IMongoDatabase db, string collectionName)
        {
            _db = db;
            _collectionName = collectionName;
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Map document fields to update in database
        /// </summary>
        /// <param name="document">Document data object</param>
        protected abstract UpdateDefinition<TDocument> MapToUpdate(TDocument document);

        #endregion

        #region Public Methods

        ///<inheritdoc/>
        public Task<TDocument> GetById(string id)
        {
            FilterDefinition<TDocument> fd = Builders<TDocument>.Filter.Eq("_id", id);
            TDocument result = Collection.Find(fd).FirstOrDefault();
            return Task.FromResult(result);
        }

        ///<inheritdoc/>
        public virtual Task<IEnumerable<TDocument>> GetAll()
        {
            IEnumerable<TDocument> result = Collection.AsQueryable().ToList();
            return Task.FromResult(result);
        }

        ///<inheritdoc/>
        public virtual Task Add(TDocument document)
        {
            Collection.InsertOne(document, new InsertOneOptions());
            return Task.CompletedTask;
        }

        ///<inheritdoc/>
        public virtual Task Update(TDocument document)
        {
            FilterDefinition<TDocument> fd = Builders<TDocument>.Filter.Eq("_id", document.Id);
            UpdateDefinition<TDocument> update = MapToUpdate(document);
            Collection.UpdateOne(fd, update, null);
            return Task.CompletedTask;
        }

        ///<inheritdoc/>
        public virtual Task Remove(TDocument document)
        {
            FilterDefinition<TDocument> fd = Builders<TDocument>.Filter.Eq("_id", document.Id);
            Collection.DeleteOne(fd);
            return Task.CompletedTask;
        }

        #endregion

    }
}
