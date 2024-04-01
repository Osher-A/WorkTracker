using System;
using System.Configuration;
using System.Windows;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WorkTracker.BusinessLogic;
using WorkTracker.Data.DAL;
using WorkTracker.Data.Model;

namespace WorkTracker.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            // To add all migrations if they do not exist like when moving to another machine
            using var scope = _serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetService<WorkContext>();
            context?.Database.Migrate();
        }
        private void OnStartUp(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<View.MainWindow>();
            mainWindow?.Show();

        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Retrieve the connection string from the configuration file
            string connectionString = ConfigurationManager.ConnectionStrings["WorkDb"].ConnectionString;

            services.AddDbContext<WorkContext>(options =>
            {
                options.UseSqlServer(connectionString);
            }, ServiceLifetime.Transient);

            services.AddAutoMapper(typeof(BusinessLogic.Mapper).Assembly);
            services.AddScoped<IWorkRepository, WorkRepository>();
            services.AddSingleton<View.MainWindow>();
        }
    }
}
