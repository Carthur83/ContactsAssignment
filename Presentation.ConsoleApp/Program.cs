
using Business.Interfaces;
using Business.Repositories;
using Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.ConsoleApp.Interfaces;
using Presentation.ConsoleApp.Services;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IContactFileService>(new ContactFileService("Data", "contacts.json"));
        services.AddSingleton<IContactRepository, ContactRepository>();
        services.AddSingleton<IContactService, ContactService>();
        services.AddSingleton<IMenuService, MenuService>();
    })
    .Build();

using var scope = host.Services.CreateScope();
var menu = scope.ServiceProvider.GetRequiredService<IMenuService>();

menu.Run();