using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandValidatorTest
{
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
    {
        // arrange (Hazırlık)
        UpdateGenreCommand command = new UpdateGenreCommand(null, null);
        command.GenreId = 1;
        command.Model = new UpdateGenreModel()
        {
            Name = name
        };

        // act (Çalıştırma)
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        // arrange (Hazırlık)
        UpdateGenreCommand command = new UpdateGenreCommand(null, null);
        command.GenreId = 1;
        command.Model = new UpdateGenreModel()
        {
            Name = "Test"
        };

        // act (Çalıştırma)
        UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}