using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using RSoft.Lib.Data.MongoDb.Options;

namespace RSoft.Lib.Data.MongoDb.Abstractions
{

    /// <summary>
    /// Provides database connection for MongoDb
    /// </summary>
    public class MongoDatabaseProvider
    {

        #region Local Objects/Variables

        protected MongoDbConnectionStrings _connectionStringOptions;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new provider instance
        /// </summary>
        /// <param name="connectionStringOptions">MongoDb connection string</param>
        public MongoDatabaseProvider(IOptions<MongoDbConnectionStrings> connectionStringOptions)
        {
            _connectionStringOptions = connectionStringOptions?.Value;
        }

        #endregion

        #region Static methods

        static MongoDatabaseProvider()
        {
            ConventionPack convertionPack = new ConventionPack
            {
                new CamelCaseElementNameConvention(),
                new StringObjectIdConvention(),
                new IgnoreExtraElementsConvention(true)
            };

            ConventionRegistry.Register("conversions", convertionPack, type => true);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Get database object
        /// </summary>
        public IMongoDatabase GetDatabase()
        {
            string databaseName = MongoUrl.Create(_connectionStringOptions.MongoDb).DatabaseName;
            MongoClient databaseClient = new MongoClient(_connectionStringOptions.MongoDb);
            return databaseClient.GetDatabase(databaseName);
        }

        #endregion

    }

}
