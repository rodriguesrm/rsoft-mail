using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RSoft.Mail.Business.Contracts;
using RSoft.Mail.Business.Models;
using RSoft.Mail.Business.Options;
using RSoft.Mail.Business.Repositories;
using RSoft.Mail.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSoft.Mail.Business.Services
{

    /// <summary>
    /// Email sending service class-object
    /// </summary>
    public class MailService : IMailService
    {

        #region Local objects/variables

        private readonly IMailRepository _mailRepository;
        private readonly ISender _sender;
        private readonly bool _isProduction;
        private readonly RedirectToOptions _redirectOptions;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new mail-service instance
        /// </summary>
        /// <param name="mailRepository">Mail repository object</param>
        /// <param name="sender">Mail sender oject</param>
        public MailService(IMailRepository mailRepository, ISender sender, IOptions<RedirectToOptions> options, IConfiguration configuration)
        {
            _mailRepository = mailRepository;
            _sender = sender;
            _isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Production;
            _redirectOptions = options?.Value;
        }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public Task<(SendMailResult, string)> SendMail(IMessage message)
            => SendMail(message, null);

        ///<inheritdoc/>
        public async Task<(SendMailResult, string)> SendMail(IMessage message, IEmailAddress redirectTo)
        {

            string requestId = await _mailRepository.SaveRequestAsync(message);

            IMessageHandle msgToSend;
            if (_isProduction)
            {
                msgToSend =
                    new Message
                    (
                        message.Subject,
                        message.Content,
                        message.From,
                        message.ReplyTo,
                        message.To.ToList(),
                        message.Cc.ToList(),
                        message.Bcc.ToList(),
                        message.Files.ToList(),
                        message.EnableHtml
                    );
            }
            else
            {

                if (string.IsNullOrWhiteSpace((redirectTo ?? _redirectOptions).Email))
                    throw new ArgumentNullException("RedirectTo options parameters is not defined in application settings");

                msgToSend = 
                    new Message
                    (
                        message.Subject,
                        message.Content,
                        message.From,
                        message.ReplyTo,
                        new List<IEmailAddress>() { redirectTo ?? _redirectOptions },
                        null,
                        null,
                        message.Files.ToList(),
                        message.EnableHtml
                    );

            }

            if (message.Headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> header in message.Headers)
                {
                    msgToSend.AddHeader(header.Key, header.Value);
                }
            }

            SendMailResult mailResult = await _sender.SendMailAsync(msgToSend);

            return (mailResult, requestId);

        }

        #endregion

    }

}
