using Business.Factories;
using Business.Models;

namespace Business.Tests.Factories;

public class ContactFactory_Tests
{
    [Fact]
    public void Create_ShouldReturnNewContact()
    {
        // act
        var result = ContactFactory.Create();

        // assert
        Assert.IsType<Contact>(result);
    }
}
