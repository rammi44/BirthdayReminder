using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BirthdayReminder.Configuration.Configuration
{
    public class EmailConfiguration : ValidateConfiguration
    {
        /// <summary>
        /// email account that email are sent
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// dispaly name
        /// </summary>
        [Required]
        public string DisplayName { get; set; }

        /// <summary>
        /// host name
        /// </summary>
        [Required]
        public string Host { get; set; }

        /// <summary>
        /// port number
        /// </summary>
        [Required]
        public int Port { get; set; }

        /// <summary>
        /// password of email
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Enable ssl or not
        /// </summary>
        [Required]
        public bool EnableSsl { get; set; }

        /// <summary>
        /// If we are using send grid add key here
        /// </summary>
        public string SendGridApiKey { get; set; }
    }
}
