using BookStore.Application.BookOperation.Commands.CreateBook;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandValidatorTests
{
    [Theory]
    [InlineData("Lord Of The Rings", 0, 1, 100)]
    [InlineData("Lord Of The Rings", 0, 0, 0)]
    [InlineData("", 1, 1, 100)]
    [InlineData("", 0, 0, 0)]
    [InlineData("Lor", 1, 1, 100)]
    [InlineData("Lo", 0, 0, 0)]
    [InlineData(" ", 1, 1, 100)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int authorId, int genreId,
        int pageCount)
    {
        // arrange (Hazırlık)
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel()
        {
            Title = title,
            AuthorId = authorId,
            GenreId = genreId,
            PageCount = pageCount,
            PublishDate = DateTime.Now.Date.AddYears(-1)
        };

        // act (Çalıştırma)
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
    {
        // arrange (Hazırlık)
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel()
        {
            Title = "Lord Of The Rings",
            AuthorId = 1,
            GenreId = 1,
            PageCount = 100,
            PublishDate = DateTime.Now.Date
        };

        // act (Çalıştırma)
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
    {
        // arrange (Hazırlık)
        CreateBookCommand command = new CreateBookCommand(null, null);
        command.Model = new CreateBookModel()
        {
            Title = "Lord Of The Rings",
            AuthorId = 1,
            GenreId = 1,
            PageCount = 100,
            PublishDate = DateTime.Now.Date.AddYears(-1)
        };

        // act (Çalıştırma)
        CreateBookCommandValidator validator = new CreateBookCommandValidator();
        var result = validator.Validate(command);

        // assert (Doğrulama)
        var equals = result.Errors.Count.Should().Be(0);
    }
}