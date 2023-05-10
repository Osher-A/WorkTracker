using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Model;
using AutoMapper;
using WorkTracker.DAL;
using WorkTracker.View;

namespace WorkTracker
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
            using (var scope = _serviceProvider.CreateScope())
            using (var context = scope.ServiceProvider.GetService<WorkContext>())
            {
                context?.Database.Migrate();
            }
        }
        private void OnStartUp(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<View.MainWindow>();
            mainWindow?.Show();

        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WorkContext>(ServiceLifetime.Transient);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IWorkRepository, WorkRepository>();
            services.AddSingleton<View.MainWindow>();
        }
    }
}
