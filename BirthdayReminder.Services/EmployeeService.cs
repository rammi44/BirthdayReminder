using BirthdayReminder.Configuration.Configuration;
using BirthdayReminder.Configuration.Entities;
using System.Collections.Generic;

namespace BirthdayReminder.Services
{
    /// <summary>
    /// Implemented GetBirthdayEmployeeAsync 
    /// connect to sql or  any database server
    /// get the data from the database
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        public EmployeeService()
        {

        }

        public List<EmployeeEntity> GetBirthdayEmployeeAsync(AppConfiguration appConfiguration)
        {
            List<EmployeeEntity> employees = new List<EmployeeEntity>();

            return employees;
        }
    }
}
