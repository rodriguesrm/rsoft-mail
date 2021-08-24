using RSoft.Mail.Business.Documents;
using RSoft.Mail.Contract;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Mail.Business.Repositories
{

    /// <summary>
    /// Mail respository object
    /// </summary>
    public class MailRepository : IMailRepository
    {

        #region Local objects/variables

        private readonly IMailDocumentRepository _mailDocumentRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a MailRepository instace
        /// </summary>
        /// <param name="mailDocumentRepository">MailDocument repository instance</param>
        public MailRepository(IMailDocumentRepository mailDocumentRepository)
        {
            _mailDocumentRepository = mailDocumentRepository;
        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public Task<string> SaveRequestAsync(IMessage message, CancellationToken cancellationToken = default)
        {

            MailDocument mailDocument = new MailDocument()
            {
                From = new MailDocument.EmailAddressDocument() { Name = message.From.Name, Email = message.From.Email },
                To =message.To?.Select(t => new MailDocument.EmailAddressDocument() { Name = t.Name, Email = t.Email }),
                Bcc = message.Bcc?.Select(b => new MailDocument.EmailAddressDocument() { Email = b.Email, Name = b.Name }),
                Cc = message.Cc?.Select(c => new MailDocument.EmailAddressDocument() { Email = c.Email, Name = c.Name }),
                Subject = message.Subject,
                Content = message.Content,
                EnableHtml = message.EnableHtml
            };

            if (message.ReplyTo != null)
                mailDocument.ReplyTo = new MailDocument.EmailAddressDocument() { Name = message.ReplyTo.Name, Email = message.ReplyTo.Email };

            if (message.Files?.Count > 0)
                mailDocument.Files = message.Files.Select(f => new MailDocument.FileAttachmentDocument() { Type = f.Type, Filename = f.Filename, Content = f.Content });

            _mailDocumentRepository.Add(mailDocument);

            return Task.FromResult(mailDocument.Id);

        }

        #endregion

    }
}
