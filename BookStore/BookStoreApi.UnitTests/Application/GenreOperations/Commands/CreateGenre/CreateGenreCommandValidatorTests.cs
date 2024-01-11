using BookStore.Application.GenreOperations.Commands.CreateGenre;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandValidatorTests
{
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
    {
        // arrange (Hazırlık)
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        command.Model = new CreateGenreModel()
        {
            Name = name
        };

        // act (Çalıştırma)
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        // arrange (Hazırlık)
        CreateGenreCommand command = new CreateGenreCommand(null, null);
        command.Model = new CreateGenreModel()
        {
            Name = "Test"
        };

        // act (Çalıştırma)
        CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}