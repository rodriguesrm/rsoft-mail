﻿using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RSoft.Mail.Web.Api.Annotations
{

    /// <summary>
    /// Base64 validation attribute
    /// </summary>
    public class Base64Attribute : ValidationAttribute
    {

        //TODO: Move this attribute to Framework.Web
        #region Overrides

        ///<inheritdoc/>
        public override bool IsValid(object value)
        {

            string content = value as string;
            content = content.Trim();

            bool valid =
                !string.IsNullOrWhiteSpace(content)
                && (content.Length % 4 == 0)
                && Regex.IsMatch(content, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

            return valid;

        }

        #endregion

    }
}
