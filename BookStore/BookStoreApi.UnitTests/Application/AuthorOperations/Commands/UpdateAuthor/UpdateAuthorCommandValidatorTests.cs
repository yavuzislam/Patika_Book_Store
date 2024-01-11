using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidatorTests
{
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
    {
        // arrange (Hazırlık)
        UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
        command.AuthorId = 1;
        command.Model = new UpdateAuthorModel()
        {
            Name = name
        };

        // act (Çalıştırma)
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        // arrange (Hazırlık)
        UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
        command.AuthorId = 1;
        command.Model = new UpdateAuthorModel()
        {
            Name = "Test"
        };

        // act (Çalıştırma)
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}