using RSoft.Lib.Data.MongoDb.Repositories;
using RSoft.Mail.Business.Documents;

namespace RSoft.Mail.Business.Repositories
{

    /// <summary>
    /// Mail document repository interface contract
    /// </summary>
    public interface IMailDocumentRepository : IDocumentRepository<MailDocument>
    {
    }

}