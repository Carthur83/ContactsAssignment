﻿using Business.Models;

namespace Business.Interfaces;

public interface IContactService
{
    bool AddContact(Contact contact);
    IEnumerable<Contact> GetAllContacts();
    Contact GetContactById(string id);
    bool DeleteContact(string id);
    bool UpdateContact(Contact updatedContact);
}
