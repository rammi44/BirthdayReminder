using BirthdayReminder.Configuration.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayReminder.Services
{
    public interface IAlertService
    {
        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailConfiguration">Email account configuration</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="cc">CC addresses list</param>
        void SendEmail(EmailConfiguration emailConfiguration, string subject, string body,
        string toAddress, string toName,
        IEnumerable<string> cc = null,
        string attachmentFilePath = null, string attachmentFileName = null);

        /// <summary>
        /// Send mail using send grid
        /// </summary>
        /// <param name="emailConfiguration">Email account configuration</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="cc">CC addresses list</param>
        Task SendMailUsingSendGrid(EmailConfiguration emailConfiguration, string subject, string body,
            string toAddress, string toName,
            IEnumerable<string> cc = null,
            string attachmentFilePath = null, string attachmentFileName = null);
    }
}
