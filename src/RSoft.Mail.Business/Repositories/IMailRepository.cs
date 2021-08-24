using RSoft.Mail.Contract;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RSoft.Mail.Business.Repositories
{

    /// <summary>
    /// Email Sending repository interface
    /// </summary>
    public interface IMailRepository
    {

        /// <summary>
        /// Save data request in database
        /// </summary>
        /// <param name="message">Message detail</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete</param>
        Task<string> SaveRequestAsync(IMessage message, CancellationToken cancellationToken = default);
    }
}
