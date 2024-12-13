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
        return _contacts.FirstOrDefault(x => x.Id == id)!;
    }
}
