using RSoft.Lib.Web.Annotations;
using System.ComponentModel.DataAnnotations;

namespace RSoft.Mail.Web.Api.Model.Request.v1_0
{

    /// <summary>
    /// File attachment model request
    /// </summary>
    public class FileAttachmentRequest
    {

        /// <summary>
        /// Filename of the attachment.
        /// </summary>
        [Required(ErrorMessage = "FILENAME_REQUIRED")]
        public string Filename { get; set; }

        /// <summary>
        /// Mime type of the content you are attaching. For example, application/pdf
        /// </summary>
        [Required(ErrorMessage = "TYPE_REQUIRED")]
        public string Type { get; set; }

        /// <summary>
        /// Base64 expression encoded content of the attachment.
        /// </summary>
        [Required(ErrorMessage = "FILE_CONTENT_REQUIRED")]
        [Base64(ErrorMessage = "FILE_CONTENT_INVALID_BASE64")]
        public string Content { get; set; }

    }
}
