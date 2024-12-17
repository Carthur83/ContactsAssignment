using Business.Interfaces;
using Business.Models;
using Business.Repositories;
using Moq;
using System.Text.Json;

namespace Business.Tests.Repositories;

public class ContactRepository_Tests
{
    private readonly Mock<IContactFileService> _contactFileServiceMock;
    private readonly IContactRepository _contactRepository;

    public ContactRepository_Tests()
    {
        _contactFileServiceMock = new Mock<IContactFileService>();
        _contactRepository = new ContactRepository(_contactFileServiceMock.Object);
    }

    [Fact]
    // arrange
    public void SaveListToFile_ShouldSaveListOfContactsToFileAndReturnTrue_WhenSaveIsSuccesful()
    {
        // arrange
        List<Contact> contacts = [];
        _contactFileServiceMock
            .Setup(x => x.SaveToFile(It.IsAny<string>()))
            .Returns(true);

        // act
        var result = _contactRepository.SaveListToFile(contacts);

        // assert
        Assert.True(result);
    }

    [Fact]
    public void GetListFromFile_ShouldReturnListOfContacts()
    {
        // arrange
        var contacts = new List<Contact>() 
        {
            new Contact { FirstName = "test1" },
            new Contact { FirstName = "test2" }       
        };
        var json = JsonSerializer.Serialize(contacts);
        _contactFileServiceMock
            .Setup(x => x.LoadFromFile())
            .Returns(json);

        // act
        var result = _contactRepository.GetListFromFile();

        // assert
        Assert.IsType<List<Contact>>(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("test1", contacts.First().FirstName);
    }

    [Fact]
    public void GetListFromFile_ShouldReturnNewList_WhenStringContentIsNull()
    {
        // arrange
        string item = null!;
        _contactFileServiceMock
            .Setup(x => x.LoadFromFile())
            .Returns(item);
        // act
        var result = _contactRepository.GetListFromFile();

        // assert
        Assert.NotNull(result);
        Assert.IsType<List<Contact>>(result);
    }
}
