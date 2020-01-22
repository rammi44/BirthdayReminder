using System;
using System.Collections.Generic;
using System.Text;

namespace BirthdayReminder.Configuration.Entities
{
    public class EmployeeEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
    }
}
