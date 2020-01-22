using Autofac;
using Autofac.Extensions.DependencyInjection;
using BirthdayReminder.Configuration.Configuration;
using BirthdayReminder.Configuration.Exceptions;
using BirthdayReminder.Configuration.Infrastructure;
using BirthdayReminder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayReminder
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");

            var config = builder.Build();

            var appConfig = config.GetSection("appConfiguration").Get<AppConfiguration>();
            var emailConfig = config.GetSection("emailConfiguration").Get<EmailConfiguration>();

            RegisterServices();

            var alertService = _serviceProvider.GetService<IAlertService>();
            var empService = _serviceProvider.GetService<IEmployeeService>();

            try
            {
                Console.WriteLine("Birthday reminder app!");

                //Validating configuration
                appConfig.Validate();
                emailConfig.Validate();

                var bdayEmployees = empService.GetBirthdayEmployeeAsync(appConfig);

                bdayEmployees.ForEach(emp =>
                {
                    //String.Format(appConfig.Template.GetRandom(), emp.Name)
                    var birthdayBody = appConfig.Template.GetRandom().Replace("{{emp_name}}", emp.Name);
                    var task = Task.Run(async () => await alertService.SendMailUsingSendGrid(emailConfig, appConfig.MailSubject, birthdayBody, emp.Email, emp.Name, appConfig.CcList, null, null));
                    task.Wait();

                    Console.WriteLine("sent email to {0}", emp.Name);
                });

                DisposeServices();

                Console.WriteLine("Close Birthday reminder app!");
            }
            catch (Exception ex)
            {
                ex.LogError(appConfig.LogFilePath);
                if (appConfig.IsAlertEmailEnabled && (ex.GetType() != typeof(ValidationException) || ex.GetType() == typeof(EmailSendingException)))
                {
                    alertService.SendMailUsingSendGrid(emailConfig, Common.ErrorMailSubject, ex.Message, appConfig.AlertMailId, Common.AdminName, null, null, null);
                }
                DisposeServices();
            }

            Console.Read();
        }

        private static void RegisterServices()
        {
            var collection = new ServiceCollection();

            //registering services using autofac
            var builder = new ContainerBuilder();
            builder.RegisterType<AlertService>().As<IAlertService>();
            builder.RegisterType<ExcelEmployeeService>().As<IEmployeeService>();
            //
            // Add other services ...
            //
            builder.Populate(collection);
            var appContainer = builder.Build();
            _serviceProvider = new AutofacServiceProvider(appContainer);
        }

        /// <summary>
        /// Dispose services
        /// </summary>
        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
