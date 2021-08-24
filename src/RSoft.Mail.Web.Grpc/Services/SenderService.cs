using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RSoft.Mail.Business.Models;
using RSoft.Mail.Business.Services;
using RSoft.Mail.Web.Grpc.Contracts;
using RSoft.Mail.Web.Grpc.Host.Extensions;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RSoft.Mail.Web.Grpc.Host
{
    public class SenderService : MailSender.MailSenderBase
    {

        #region Local objects/variables

        private readonly IMailService _mailService;
        private readonly ILogger<SenderService> _logger;

        #endregion

        #region Constructors

        public SenderService(IMailService mailService, ILogger<SenderService> logger) : base()
        {
            _mailService = mailService;
            _logger = logger;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Send mail
        /// </summary>
        /// <param name="request">Request data</param>
        /// <param name="context">Server call context</param>
        [Authorize]
        public override async Task<MailSendReply> Send(MailSendRequest request, ServerCallContext context)
        {

            _logger.LogInformation("gRPC SendMail - START", request);

            EmailAddress redirect = null;

            if (!string.IsNullOrWhiteSpace(request.RedirectTo) && Regex.IsMatch(request.RedirectTo, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                redirect = new EmailAddress(request.RedirectTo);

            Message message = request.Map();
            (SendMailResult result, string mailId) = await _mailService.SendMail(message, redirect);

            string errors = string.Empty;
            if (result.Success)
            {
                _logger.LogInformation
                (
                    "gRPC SendMail - Email Sent to {recipient}",
                    redirect?.Email ?? string.Join(",", request.To.Select(item => item.Address).ToList())
                );
            }
            else
            {
                errors = string.Join("|", result.Errors.Select(item => $"{item.Key}=>{item.Value}").ToList());
                _logger.LogInformation
                (
                    "gRPC SendMail - Fail to send e-mail to {recipient}, Error: {error}",
                    redirect?.Email ?? string.Join(",", request.To.Select(item => item.Address).ToList()),
                    errors
                );
            }

            _logger.LogInformation("gRPC SendMail - END");

            return new MailSendReply()
            {
                Success = result.Success,
                MailId = mailId,
                Errors = errors
            };

        }

        #endregion

    }
}
