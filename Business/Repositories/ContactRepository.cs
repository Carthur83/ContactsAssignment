using Business.Interfaces;
using Business.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Business.Repositories;

public class ContactRepository(IContactFileService contactFileService) : IContactRepository
{
    private readonly IContactFileService _contactFileService = contactFileService;
    public bool SaveListToFile(List<Contact> list)
    {
        try
        {
            var json = JsonSerializer.Serialize(list);
            _contactFileService.SaveToFile(json);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public List<Contact> GetListFromFile()
    {
        var json = _contactFileService.LoadFromFile();
        return JsonSerializer.Deserialize<List<Contact>>(json) ?? [];
    }
}
