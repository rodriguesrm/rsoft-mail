﻿using System.Collections.Generic;

namespace RSoft.Mail.Contract
{

    /// <summary>
    /// Message sending interface
    /// </summary>
    public interface IMessage
    {

        #region Properties

        /// <summary>
        /// A list of Message Headers
        /// </summary>
        IReadOnlyDictionary<string, string> Headers { get; }

        /// <summary>
        /// Get sender's e-mail
        /// </summary>
        IEmailAddress From { get; }

        /// <summary>
        /// Get reply to e-mail
        /// </summary>
        IEmailAddress ReplyTo { get; }

        /// <summary>
        /// Get recipient's e-mail
        /// </summary>
        IReadOnlyList<IEmailAddress> To { get; }

        /// <summary>
        /// CC e-mail recipient
        /// </summary>
        IReadOnlyList<IEmailAddress> Cc { get; }

        /// <summary>
        /// BCC e-mail recipient
        /// </summary>
        IReadOnlyList<IEmailAddress> Bcc { get; }

        /// <summary>
        /// Getsthe subject of your email.
        /// </summary>
        string Subject { get; }

        /// <summary>
        /// Message content
        /// </summary>
        string Content { get; }

        /// <summary>
        /// List of files to be sent as an attachment
        /// </summary>
        IReadOnlyList<IFileAttachment> Files { get; }

        /// <summary>
        /// Indicates whether the message will be sent in html format
        /// </summary>
        public bool EnableHtml { get; }

        #endregion

    }
}
