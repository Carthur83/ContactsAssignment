using Business.Interfaces;
using Business.Models;
using Business.Services;
using Moq;

namespace Business.Tests.Services;

public class ContactService_Tests
{
    private readonly Mock<IContactRepository> _contactRepositoryMock;
    private readonly IContactService _contactService;

    public ContactService_Tests()
    {
        _contactRepositoryMock = new Mock<IContactRepository>();
        _contactService = new ContactService(_contactRepositoryMock.Object);
    }

    [Fact]
    public void AddContact_ShouldReturnTrue_WhenContactIsAddedSuccessfully()
    {
        // arrange
        var contact = new Contact { FirstName = "Test", Email = "test@domain.com"};

        _contactRepositoryMock
            .Setup(x => x.SaveListToFile(It.IsAny<List<Contact>>()))
            .Returns(true);

        // act
        var result = _contactService.AddContact(contact);

        // assert
        Assert.True(result);
        _contactRepositoryMock.Verify(x => x.SaveListToFile(It.IsAny<List<Contact>>()), Times.Once);
    }

    [Fact]
    public void GetAllContacts_ShouldReturnIEnumerableListOfContacts()
    {
        // arrange
        List<Contact> contacts =
        [
            new Contact { FirstName = "Test", Email = "test@domain.com" },
            new Contact { FirstName = "Test2", Email = "test2@domain.com" }
        ];
        
        _contactRepositoryMock
            .Setup(x => x.GetListFromFile())
            .Returns(contacts);

        // act
        var result = _contactService.GetAllContacts();

        // assert
        Assert.IsAssignableFrom<IEnumerable<Contact>>(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(contacts[0], result.First());
    }

    [Fact]
    public void GetContactById_ShouldReturnCorrectContactFromList()
    {
        // arrange
        var id1 = Guid.NewGuid().ToString();
        List<Contact> contacts =
        [
            new Contact { Id = id1, FirstName = "Test", Email = "test@domain.com" }
        ];
        
        _contactRepositoryMock
            .Setup(x => x.GetListFromFile())
            .Returns(contacts);

        // act
        var result = _contactService.GetContactById(id1);

        // assert
        Assert.Equal(id1, result.Id);
    }
}


