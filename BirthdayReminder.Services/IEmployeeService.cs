using BirthdayReminder.Configuration.Configuration;
using BirthdayReminder.Configuration.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayReminder.Services
{
    public interface IEmployeeService
    {
        List<EmployeeEntity> GetBirthdayEmployeeAsync(AppConfiguration appConfiguration);
    }
}
