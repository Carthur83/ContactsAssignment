using Business.Models;

namespace Business.Interfaces
{
    public interface IContactRepository
    {
        List<Contact> GetListFromFile();
        bool SaveListToFile(List<Contact> list);
    }
}