using RSoft.Mail.Business.Models;
using RSoft.Mail.Contract;
using System.Threading.Tasks;

namespace RSoft.Mail.Business.Services
{
    public interface IMailService
    {

        /// <summary>
        /// Send e-mail
        /// </summary>
        /// <param name="message">Message details</param>
        Task<(SendMailResult, string)> SendMail(IMessage message);

        /// <summary>
        /// Send e-mail
        /// </summary>
        /// <param name="message">Message details</param>
        /// <param name="redirectTo">E-mail address to redirect all e-mails</param>
        Task<(SendMailResult, string)> SendMail(IMessage message, IEmailAddress redirectTo);

    }
}