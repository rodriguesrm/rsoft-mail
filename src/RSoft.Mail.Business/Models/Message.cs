﻿using RSoft.Mail.Business.Contracts;
using RSoft.Mail.Contract;
using System.Collections.Generic;
using System.Linq;

namespace RSoft.Mail.Business.Models
{

    /// <summary>
    /// Mail message object
    /// </summary>
    public class Message : IMessageHandle
    {

        #region Local objects/variables

        private readonly Dictionary<string, string> _headers;
        private readonly List<IEmailAddress> _to;
        private readonly List<IEmailAddress> _cc;
        private readonly List<IEmailAddress> _bcc;
        private readonly List<IFileAttachment> _files;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new message instance
        /// </summary>
        /// <param name="subject">E-mail subject</param>
        /// <param name="content">E-mail content</param>
        /// <param name="from">Sender's e-mail</param>
        /// <param name="replyTo">ReplyTo's e-mail</param>
        /// <param name="to">Recipient's e-mails</param>
        /// <param name="cc">Cc recipient's e-mails</param>
        /// <param name="bcc">Bcc recipient's e-mails</param>
        /// <param name="files">Files to attach</param>
        /// <param name="enableHtml">Indicates whether the message will be sent in html format</param>
        public Message(string subject, string content, IEmailAddress from, IEmailAddress replyTo, IEnumerable<IEmailAddress> to, IEnumerable<IEmailAddress> cc, IEnumerable<IEmailAddress> bcc, IEnumerable<IFileAttachment> files, bool enableHtml)
        {
            Subject = subject;
            Content = content;
            From = from;
            ReplyTo = replyTo;
            _to = to.ToList();
            _cc = cc?.ToList() ?? new List<IEmailAddress>();
            _bcc = bcc?.ToList() ?? new List<IEmailAddress>();
            _files = files?.ToList() ?? new List<IFileAttachment>();
            EnableHtml = enableHtml;
            _headers = new Dictionary<string, string>();
        }

        #endregion

        #region Properties

        ///<inheritdoc/>
        public IReadOnlyDictionary<string, string> Headers => _headers;

        ///<inheritdoc/>
        public IEmailAddress From { get; private set; }

        ///<inheritdoc/>
        public IEmailAddress ReplyTo { get; private set; }

        ///<inheritdoc/>
        public IReadOnlyList<IEmailAddress> To => _to.AsReadOnly();

        ///<inheritdoc/>
        public IReadOnlyList<IEmailAddress> Cc => _cc.AsReadOnly();

        ///<inheritdoc/>
        public IReadOnlyList<IEmailAddress> Bcc => _bcc.AsReadOnly();

        ///<inheritdoc/>
        public string Subject { get; private set; }

        ///<inheritdoc/>
        public string Content { get; private set; }

        ///<inheritdoc/>
        public IReadOnlyList<IFileAttachment> Files => _files.AsReadOnly();
        
        ///<inheritdoc/>
        public bool EnableHtml { get; private set; }

        #endregion

        #region Public methods

        ///<inheritdoc/>
        public void AddBccRecipient(IEmailAddress recipient)
        {
            if (_bcc.FirstOrDefault(x => x.Email == recipient.Email) == null)
                _bcc.Add(recipient);
        }

        ///<inheritdoc/>
        public void AddCcRecipient(IEmailAddress recipient)
        {
            if (_cc.FirstOrDefault(x => x.Email == recipient.Email) == null)
                _cc.Add(recipient);
        }

        ///<inheritdoc/>
        public void AddRecipient(IEmailAddress recipient)
        {
            if (_to.FirstOrDefault(x => x.Email == recipient.Email) == null)
                _to.Add(recipient);
        }

        ///<inheritdoc/>
        public void AddAttachment(IFileAttachment file)
        {
            if (_files.FirstOrDefault(x => x.Filename.ToLower() == file.Filename.ToLower()) == null)
                _files.Add(file);
        }

        ///<inheritdoc/>
        public void AddHeader(string key, string value)
            => _headers.TryAdd(key, value);

        #endregion

    }

}
