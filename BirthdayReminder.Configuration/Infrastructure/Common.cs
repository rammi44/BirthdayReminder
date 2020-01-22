using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BirthdayReminder.Configuration.Infrastructure
{
    /// <summary>
    /// It contains all common and extension methods
    /// </summary>
    public static class Common
    {
        public const string ErrorMailSubject = "Birthday Reminder App Error Mail"; 
        public const string AdminName = "Admin";

        /// <summary>
        /// extension method for random object from the list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static T GetRandom<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            // note: creating a Random instance each call may not be correct for you,
            // consider a thread-safe static instance
            var r = new Random();
            var list = enumerable as IList<T> ?? enumerable.ToList();
            return list.Count == 0 ? default(T) : list[r.Next(0, list.Count)];
        }

        /// <summary>
        /// Log writer
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="path">path</param>
        public static void LogError(this Exception ex, string path)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string writePath = Path.Combine(path, DateTime.Now.Ticks + "_" + "ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(writePath, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}
