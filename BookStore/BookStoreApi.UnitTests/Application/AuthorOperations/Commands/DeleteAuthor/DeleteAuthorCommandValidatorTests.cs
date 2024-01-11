using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandValidatorTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId)
    {
        // arrange (Hazırlık)
        DeleteAuthorCommand command = new DeleteAuthorCommand(null);
        command.AuthorId = authorId;

        // act (Çalıştırma)
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(int authorId)
    {
        // arrange (Hazırlık)
        DeleteAuthorCommand command = new DeleteAuthorCommand(null);
        command.AuthorId = authorId;

        // act (Çalıştırma)
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}