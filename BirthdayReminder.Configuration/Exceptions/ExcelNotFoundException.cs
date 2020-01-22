using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayReminder.Configuration.Exceptions
{
    /// <summary>
    /// Excel not found exception
    /// </summary>
    [Serializable]
    public class ExcelNotFoundException : Exception
    {
        public ExcelNotFoundException()
        {

        }

        public ExcelNotFoundException(string path) : base(String.Format("Excel is not found at {0}", path))
        {

        }
    }
}
