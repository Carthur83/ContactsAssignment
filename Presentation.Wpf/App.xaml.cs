using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Wpf.ViewModels;
using Presentation.Wpf.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Presentation.Wpf
{
   
    public partial class App : Application
    {
        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IContactFileService>(new ContactFileService("Data", "contacts.json"));
                    services.AddSingleton<IContactRepository, ContactRepository>();
                    services.AddSingleton<IContactService, ContactService>();

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>();

                    services.AddTransient<ContactListViewModel>();
                    services.AddTransient<ContactListView>();

                    services.AddTransient<ContactAddViewModel>();
                    services.AddTransient<ContactAddView>();

                    services.AddTransient<ContactDetailsViewModel>();
                    services.AddTransient<ContactDetailsView>();

                    services.AddTransient<ContactUpdateViewModel>();
                    services.AddTransient<ContactUpdateView>();

                })                
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainMenu = _host.Services.GetRequiredService<MainWindow>();
            mainMenu.Show();
        }
    }

}
