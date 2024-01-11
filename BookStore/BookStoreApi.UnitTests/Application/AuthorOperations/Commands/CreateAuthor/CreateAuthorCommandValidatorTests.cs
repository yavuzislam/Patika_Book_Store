using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidatorTests
{
    [Theory]
    [InlineData(" ")]
    [InlineData("")]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
    {
        // arrange (Hazırlık)
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel()
        {
            Name = name
        };

        // act (Çalıştırma)
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        // arrange (Hazırlık)
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel()
        {
            Name = "Test"
        };

        // act (Çalıştırma)
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}