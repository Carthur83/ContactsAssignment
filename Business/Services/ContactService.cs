using Business.Helpers;
using Business.Interfaces;
using Business.Models;
using System.Diagnostics;

namespace Business.Services;

public class ContactService(IContactRepository contactRepository) : IContactService
{
    private readonly IContactRepository _contactRepository = contactRepository;
    private List<Contact> _contacts = [];

    public bool AddContact(Contact contact)
    {
        try
        {
            contact.Id = IdGenerator.Generate();
            _contacts.Add(contact);
            _contactRepository.SaveListToFile(_contacts);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public IEnumerable<Contact> GetAllContacts()
    {
        _contacts = _contactRepository.GetListFromFile();
        return _contacts;
    }

    public Contact GetContactById(string id)
    {
        _contacts = _contactRepository.GetListFromFile();
        var contact = _contacts.FirstOrDefault(x => x.Id == id)!;
        return contact;
    }

    public bool DeleteContact(string id)
    {
        if (id == null)
        {
            return false;
        }

        _contacts = _contactRepository.GetListFromFile();
        var result = _contacts.RemoveAll(x => x.Id == id);

        if (result > 0)
        {
            _contactRepository.SaveListToFile(_contacts);
            return true;
        }
        return false;
    }

    public bool UpdateContact(Contact updatedContact)
    {
        var contact = GetContactById(updatedContact.Id);

        if (contact == null) { return false; }

        contact.FirstName = updatedContact.FirstName;
        contact.LastName = updatedContact.LastName;
        contact.Email = updatedContact.Email;
        contact.PhoneNumber = updatedContact.PhoneNumber;
        contact.Street = updatedContact.Street;
        contact.ZipCode = updatedContact.ZipCode;
        contact.City = updatedContact.City;

        _contactRepository.SaveListToFile(_contacts);
        return true;
    }
}
