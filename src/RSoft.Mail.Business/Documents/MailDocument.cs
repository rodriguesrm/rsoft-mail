using RSoft.Lib.Data.MongoDb.Documents;
using System;
using System.Collections.Generic;

namespace RSoft.Mail.Business.Documents
{

    /// <summary>
    /// Mail document object class
    /// </summary>
    public class MailDocument : DocumentBase
    {

        #region Properties

        //TODO: Identify request / response (by sendgrid) data

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// E-mail address of the sender
        /// </summary>
        public EmailAddressDocument From { get; set; }

        /// <summary>
        /// Reply to e-mail address
        /// </summary>
        public EmailAddressDocument ReplyTo { get; set; }

        /// <summary>
        /// Recipient list
        /// </summary>
        public IEnumerable<EmailAddressDocument> To { get; set; } = new List<EmailAddressDocument>();

        /// <summary>
        /// CC recipient list
        /// </summary>
        public IEnumerable<EmailAddressDocument> Cc { get; set; } = new List<EmailAddressDocument>();

        /// <summary>
        /// BCC recipient list
        /// </summary>
        public IEnumerable<EmailAddressDocument> Bcc { get; set; } = new List<EmailAddressDocument>();

        /// <summary>
        /// E-mail subject
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// E-mail content
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// List of files to be sent as attached
        /// </summary>
        public IEnumerable<FileAttachmentDocument> Files { get; set; } = new List<FileAttachmentDocument>();

        /// <summary>
        /// Indicates whether the message will be sent in html format
        /// </summary>
        public bool EnableHtml { get; }

        #endregion

        #region Subclasses

        /// <summary>
        /// File attachment document
        /// </summary>
        public class FileAttachmentDocument
        {

            /// <summary>
            /// Attachment filename
            /// </summary>
            public string Filename { get; }

            /// <summary>
            /// Mime type of the content you are attaching. For example, application/pdf
            /// </summary>
            public string Type { get; }

            /// <summary>
            /// Base64 encoded content of the attachment.
            /// </summary>
            public string Content { get; }

        }

        /// <summary>
        /// Email address document
        /// </summary>
        public class EmailAddressDocument
        {

            /// <summary>
            /// E-mail address of the sender or recipient
            /// </summary>
            public string Email { get; }

            /// <summary>
            /// Name of the sender or recipient
            /// </summary>
            public string Name { get; }

        }

        public class MailHeaderDocument
        {
            /// <summary>
            /// Header key
            /// </summary>
            public string Key { get; set; }

            /// <summary>
            /// Header value
            /// </summary>
            public string Value { get; set; }

        }

        #endregion

    }

}
