using BirthdayReminder.Configuration.Configuration;
using BirthdayReminder.Configuration.Entities;
using Ganss.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BirthdayReminder.Configuration.Exceptions;

namespace BirthdayReminder.Services
{
    /// <summary>
    /// Implemented GetBirthdayEmployeeAsync 
    /// read the data from the excel sheet
    /// get the data from the excel
    /// </summary>
    public class ExcelEmployeeService : IEmployeeService
    {
        public ExcelEmployeeService()
        {

        }

        /// <summary>
        /// Get current day birthday employees
        /// </summary>
        /// <param name="appConfiguration">app configuration from appsettings.json</param>
        /// <returns></returns>
        public List<EmployeeEntity> GetBirthdayEmployeeAsync(AppConfiguration appConfiguration)
        {
            if (!File.Exists(Path.Combine(appConfiguration.ExcelPath, appConfiguration.ExcelName)))
                throw new ExcelNotFoundException(Path.Combine(appConfiguration.ExcelPath, appConfiguration.ExcelName));

            try
            {
                var employees = new ExcelMapper(Path.Combine(appConfiguration.ExcelPath, appConfiguration.ExcelName)).Fetch<EmployeeEntity>()
      .Where(emp => emp.DOB.Date == DateTime.Now.Date && emp.DOB.Month == DateTime.Now.Month && !appConfiguration.ExcludeEmailIds.Contains(emp.Email)).ToList();

                return employees;
            }
            catch (Exception ex)
            {
                throw new ExcelDataValidationException(ex.Message);
            }

        }
    }
}
