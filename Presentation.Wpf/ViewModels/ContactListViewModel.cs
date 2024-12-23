using Business.Interfaces;
using Business.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation.Wpf.ViewModels;

public partial class ContactListViewModel : ObservableObject
{
    private readonly IContactService _contactService;
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private ObservableCollection<Contact> _contacts = [];

    public ContactListViewModel(IContactService contactService, IServiceProvider serviceProvider)
    {
        _contactService = contactService;
        _serviceProvider = serviceProvider;

        _contacts = new ObservableCollection<Contact>(_contactService.GetAllContacts());
    }

    [RelayCommand]
    public void GoToContactAdd()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactAddViewModel>();
    }

    [RelayCommand]
    public void GoToContactDetails(Contact contact)
    {
        var contactDetailsViewModel = _serviceProvider.GetRequiredService<ContactDetailsViewModel>();
        contactDetailsViewModel.Contact = contact;

        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = contactDetailsViewModel;
    }
}
