using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandValidatorTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
    {
        // arrange (Hazırlık)
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.Id = id;

        // act (Çalıştırma)
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        // arrange (Hazırlık)
        DeleteGenreCommand command = new DeleteGenreCommand(null);
        command.Id = 1;

        // act (Çalıştırma)
        DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}