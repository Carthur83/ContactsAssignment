using Business.Factories;
using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Wpf.ViewModels;

public partial class ContactDetailsViewModel(IServiceProvider serviceProvider, IContactService contactService) : ObservableObject
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IContactService _contactService = contactService;

    [ObservableProperty]
    private Contact _contact = ContactFactory.Create();

    [RelayCommand]
    public void Delete(string id)
    {
        var result = _contactService.DeleteContact(id);
        if (result)
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
        }
    }

    [RelayCommand]
    public void Update()
    {
        var contactUpdateViewModel = _serviceProvider.GetRequiredService<ContactUpdateViewModel>();
        contactUpdateViewModel.Contact = Contact;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = contactUpdateViewModel;
    }

    [RelayCommand]
    public void GoToContactList()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactListViewModel>();
    }
}


