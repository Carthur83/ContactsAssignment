using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Presentation.ConsoleApp.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Presentation.ConsoleApp.Services;

public class MenuService(IContactService contactService) : IMenuService
{
    private readonly IContactService _contactService = contactService;

    public void Run()
    {
        while (true)
        {
            MainMenu();
        }
    }
    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("----- Main Menu -----");

        Console.WriteLine("1. Create New Contact");
        Console.WriteLine("2. Show All Contacts");
        Console.WriteLine("3. Exit");

        string option = Console.ReadLine()!;

        switch (option)
        {
            case "1":
                CreateNewOption();
                break;

            case "2":
                ShowAllOption();
                break;

            case "3":
                QuitOption();
                break;

            default:
                ShowErrorMesage();
                break;
        }
    }

    public void CreateNewOption()
    {
        Contact contact = ContactFactory.Create();

        Console.Clear();
        Console.WriteLine("----- Create New Contact -----");
        Console.WriteLine();

        contact.FirstName = ValidateInput("First Name: ",nameof(contact.FirstName));
        contact.LastName = ValidateInput("Last Name: ", nameof(contact.LastName));
        contact.Email = ValidateInput("Email: ", nameof(contact.Email));
        contact.PhoneNumber = ValidateInput("Phone Number: ", nameof(contact.PhoneNumber));
        contact.Street = ValidateInput("Street: ", nameof(contact.Street));
        contact.ZipCode = ValidateInput("Zip Code: ", nameof(contact.ZipCode));
        contact.City = ValidateInput("City: ", nameof(contact.City));

        bool isAdded = _contactService.AddContact(contact);

        if (isAdded)
        {
            Console.WriteLine("Contact added succesfully");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Something went wrong, no contact added");
            Console.ReadKey();
        }
    }

    public void ShowAllOption()
    {
        var list = _contactService.GetAllContacts();

        Console.Clear();
        Console.WriteLine("----- All Contacts -----");
        Console.WriteLine();

        if (!list.Any())
        {
            Console.WriteLine("No contacts found");
        }

        foreach (var contact in list)
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"First Name: {contact.FirstName}");
            Console.WriteLine($"Last Name: {contact.LastName}");
            Console.WriteLine($"Email: {contact.Email}");
            Console.WriteLine($"Phone Number: {contact.PhoneNumber}");
            Console.WriteLine($"Street: {contact.Street}");
            Console.WriteLine($"Zip Code: {contact.ZipCode}");
            Console.WriteLine($"City: {contact.City}");
            Console.WriteLine("----------------------------------");
        }

        Console.ReadKey();
    }

    public void QuitOption()
    {
        Environment.Exit(0);
    }

    public void ShowErrorMesage()
    {
        Console.WriteLine("Invalid input, try again");
    }

    public string ValidateInput(string message, string propertyName)
    {
        while (true)
        {
            Console.WriteLine(message);
            var input = Console.ReadLine() ?? string.Empty;

            var results = new List<ValidationResult>();
            var context = new ValidationContext(new Contact()) { MemberName = propertyName };

            if (Validator.TryValidateProperty(input, context, results))
            {
                return input;
            }

            Console.WriteLine($"{results[0].ErrorMessage}, Please try again.");
        }
    }
}
