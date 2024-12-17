using Business.Interfaces;
using Moq;

namespace Business.Tests.Services;

public class FileService_Tests
{

    private readonly Mock<IContactFileService> _contactFileServiceMock;
    private readonly IContactFileService _contactFileService;

    public FileService_Tests()
    {
        _contactFileServiceMock = new Mock<IContactFileService>();
        _contactFileService = _contactFileServiceMock.Object;
    }

    [Fact]
    public void SaveToFile_ShouldSaveContentToFile()
    {
        // arrange
        _contactFileServiceMock
            .Setup(x => x.SaveToFile(It.IsAny<string>()))
            .Returns(true);

        // act
        var result = _contactFileService.SaveToFile(It.IsAny<string>());

        //assert
        Assert.True(result);
    }

    [Fact]
    public void LoadFromFile_ShouldReturnStringFromFile()
    {
        // arrange
        _contactFileServiceMock
            .Setup(x => x.LoadFromFile())
            .Returns("");

        // act
        var result = _contactFileService.LoadFromFile();

        // assert
        Assert.Equal("", result);
    }
}
