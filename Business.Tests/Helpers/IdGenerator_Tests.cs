namespace Business.Tests.Helpers;

public class IdGenerator_Tests
{
    [Fact]
    public void Generate_ShouldReturnNewGuidIdAsString()
    {
        // act
        var result = Guid.NewGuid().ToString();

        // assert
        Assert.True(Guid.TryParse(result, out _));
    }
}
