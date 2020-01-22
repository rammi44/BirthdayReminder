using BirthdayReminder.Configuration.Configuration;
using BirthdayReminder.Configuration.Exceptions;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayReminder.Services
{
    /// <summary>
    /// Implementing send Email
    /// </summary>
    public class AlertService : IAlertService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AlertService()
        {

        }

        /// <summary>
        /// Sends an email
        /// </summary>
        /// <param name="emailConfiguration">Email account configuration</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="cc">CC addresses list</param>
        /// <param name="attachmentFilePath">Attachment file path</param>
        /// <param name="attachmentFileName">Attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.</param>
        public virtual void SendEmail(EmailConfiguration emailConfiguration, string subject, string body,
            string toAddress, string toName,
            IEnumerable<string> cc = null,
            string attachmentFilePath = null, string attachmentFileName = null)
        {
            try
            {
                var message = new MailMessage
                {
                    //from, to, reply to
                    From = new MailAddress(emailConfiguration.Email, emailConfiguration.DisplayName)
                };
                message.To.Add(new MailAddress(toAddress, toName));

                //CC
                if (cc != null)
                {
                    foreach (var address in cc.Where(ccValue => !string.IsNullOrWhiteSpace(ccValue)))
                    {
                        message.CC.Add(address.Trim());
                    }
                }

                //content
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                //create the file attachment for this e-mail message
                if (!string.IsNullOrEmpty(attachmentFilePath) &&
                    File.Exists(attachmentFilePath))
                {
                    var attachment = new System.Net.Mail.Attachment(attachmentFilePath);
                    if (!string.IsNullOrEmpty(attachmentFileName))
                    {
                        attachment.Name = attachmentFileName;
                    }

                    message.Attachments.Add(attachment);
                }

                //send email
                using (var smtpClient = new SmtpClient(emailConfiguration.Host))
                {
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Host = emailConfiguration.Host;
                    smtpClient.Port = emailConfiguration.Port;
                    smtpClient.EnableSsl = emailConfiguration.EnableSsl;
                    smtpClient.Credentials = new NetworkCredential(emailConfiguration.Email, emailConfiguration.Password);
                    smtpClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw new EmailSendingException(ex.Message);
            }
        }

        /// <summary>
        /// Send mail using send grid
        /// </summary>
        /// <param name="emailConfiguration">Email account configuration</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <param name="toAddress">To address</param>
        /// <param name="toName">To display name</param>
        /// <param name="cc">CC addresses list</param>
        public async Task SendMailUsingSendGrid(EmailConfiguration emailConfiguration, string subject, string body,
            string toAddress, string toName,
            IEnumerable<string> cc = null,
            string attachmentFilePath = null, string attachmentFileName = null)
        {
            try
            {
                var client = new SendGridClient(emailConfiguration.SendGridApiKey);
                var from = new EmailAddress(emailConfiguration.Email, emailConfiguration.DisplayName);
                List<EmailAddress> tos = new List<EmailAddress>
          {
              new EmailAddress(toAddress, toName)

              //new EmailAddress("test3@example.com", "Example User 3"),
              //new EmailAddress("test4@example.com","Example User 4")
          };

                //var htmlContent = "<strong>Hello world with HTML content</strong>";
                //var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", body, false);

                //CC
                if (cc != null)
                {
                    foreach (var address in cc.Where(ccValue => !string.IsNullOrWhiteSpace(ccValue)))
                    {
                        msg.AddCc(address.Trim());
                    }
                }

                var response = await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                throw new EmailSendingException(ex.Message);
            }
        }
    }
}
