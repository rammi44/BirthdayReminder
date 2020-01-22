using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayReminder.Configuration.Exceptions
{
    /// <summary>
    /// Email sending exception
    /// </summary>
    [Serializable]
    public class EmailSendingException : Exception
    {
        public EmailSendingException()
        {

        }

        public EmailSendingException(string message) : base(message)
        {

        }
    }
}
