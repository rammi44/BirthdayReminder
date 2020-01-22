using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayReminder.Configuration.Exceptions
{
    /// <summary>
    /// Email sending exception
    /// </summary>
    [Serializable]
    public class ExcelDataValidationException : Exception
    {
        public ExcelDataValidationException()
        {

        }
        public ExcelDataValidationException(string message) : base(message)
        {

        }
    }
}
