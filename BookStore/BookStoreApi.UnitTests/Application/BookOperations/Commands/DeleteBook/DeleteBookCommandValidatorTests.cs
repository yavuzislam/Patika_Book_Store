using BookStore.Application.BookOperation.Commands.DeleteBook;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandValidatorTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
    {
        // arrange (Hazırlık)
        var command = new DeleteBookCommand(null)
        {
            BookId = bookId
        };

        // act (Çalıştırma)
        var validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(int bookId)
    {
        // arrange (Hazırlık)
        var command = new DeleteBookCommand(null)
        {
            BookId = bookId
        };

        // act (Çalıştırma)
        var validator = new DeleteBookCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}