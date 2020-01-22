using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BirthdayReminder.Configuration.Configuration
{
    public class AppConfiguration : ValidateConfiguration
    {
        /// <summary>
        /// excel path
        /// </summary>
        [Required]
        public string ExcelPath { get; set; }
        [Required]
        public string ExcelName { get; set; }

        /// <summary>
        /// cc email list 
        /// </summary>
        public IEnumerable<string> CcList { get; set; }

        /// <summary>
        /// exceptional emails that you don't want send emails
        /// </summary>
        public IEnumerable<string> ExcludeEmailIds { get; set; }

        /// <summary>
        /// log file path
        /// </summary>
        [Required]
        public string LogFilePath { get; set; }

        /// <summary>
        /// subject of the email
        /// </summary>
        [Required]
        public string MailSubject { get; set; }

        /// <summary>
        /// alert emails that can send error logs
        /// </summary>
        [Required]
        public string AlertMailId { get; set; }

        /// <summary>
        /// alert emails that can send error logs
        /// </summary>
        [Required]
        public bool IsAlertEmailEnabled { get; set; }

        /// <summary>
        /// email tempaltes
        /// </summary>
        [Required]
        public IEnumerable<string> Template { get; set; }
    }
}