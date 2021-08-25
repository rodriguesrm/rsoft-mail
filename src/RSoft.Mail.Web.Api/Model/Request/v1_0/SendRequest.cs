using RSoft.Lib.Web.Annotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RSoft.Mail.Web.Api.Model.Request.v1_0
{

    /// <summary>
    /// Send mail request model
    /// </summary>
    public class SendRequest
    {

        /// <summary>
        /// Sender's e-mail
        /// </summary>
        [Required(ErrorMessage = "EMAIL_SENDER_REQUIRED")]
        public EmailRequest From { get; set; }

        /// <summary>
        /// Reply-to's e-mail
        /// </summary>
        public EmailRequest ReplyTo { get; set; }

        /// <summary>
        /// Subject of your email.
        /// </summary>
        [Required(ErrorMessage = "EMAIL_SUBJECT_REQUIRED")]
        public string Subject { get; set; }

        /// <summary>
        /// Message content
        /// </summary>
        [Required(ErrorMessage = "EMAIL_CONTENT_REQUIRED")]
        public string Content { get; set; }

        /// <summary>
        /// List of recipient's e-mail
        /// </summary>
        [Required(ErrorMessage = "EMAIL_RECIPIENT_REQUIRED")]
        [EnsureMinimumElements(1, ErrorMessage = "EMAIL_MINIMUM_RECIPIENT")]
        public IEnumerable<EmailRequest> To { get; set; }

        /// <summary>
        /// List of cc e-mail recipient
        /// </summary>
        /// <example>typeof(EmailRequest)</example>
        public IEnumerable<EmailRequest> Cc { get; set; }

        /// <summary>
        /// List of bcc e-mail recipient
        /// </summary>
        public IEnumerable<EmailRequest> Bcc { get; set; }

        /// <summary>
        /// List of files to be sent as an attachment
        /// </summary>
        public IEnumerable<FileAttachmentRequest> Files { get; set; }

        /// <summary>
        /// Indicates whether the message will be sent in html format
        /// </summary>
        public bool EnableHtml { get; set; } = true;
    }

}
