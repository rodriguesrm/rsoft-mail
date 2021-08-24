using RSoft.Lib.Data.MongoDb.Creators;
using RSoft.Mail.Business.Documents;
using System.Collections.Generic;
using System.Threading.Tasks;
using RSoft.Lib.Data.MongoDb.Extensions;
using MongoDB.Driver;

namespace RSoft.Mail.Business.Mappings
{

    /// <summary>
    /// MailDocument collection creator class
    /// </summary>
    public class MailDocumentMapping : DocumentCollectionCreatorBase<MailDocument>, IDocumentCollectionCreator
    {

        /// <summary>
        /// Create a new class instance
        /// </summary>
        /// <param name="mongoDatabase">IMongoDatabase object instance</param>
        public MailDocumentMapping(IMongoDatabase mongoDatabase) : base(mongoDatabase, nameof(MailDocument))
        {
        }

        ///<inheritdoc/>
        public override Task CreateCollection()
        {
            IList<Task> tasks = new List<Task>
            {
                CreateCollectionAsync(),
                CreateIndexAsync(field => field.Id, nameof(MailDocument.Id).ToCamelCase(), true),
                CreateIndexAsync(field => field.CreatedAtUtc, nameof(MailDocument.CreatedAtUtc).ToCamelCase(), true)
            };
            return Task.WhenAll(tasks);

        }
    }
}
