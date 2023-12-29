using BookStore.Application.BookOperation.Commands.UpdateBook;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandValidatorTests
{
    [Theory]
    [InlineData("", 1, 1, 100, 1)]
    [InlineData("Lord", 0, 1, 100, 1)]
    [InlineData("Lord", 1, 0, 100, 1)]
    [InlineData("Lord", 1, 1, 0, 1)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int authorId, int genreId,
        int pageCount, int bookId)
    {
        var command = new UpdateBookCommand(null, null)
        {
            BookId = bookId,
            Model = new UpdateBookModel()
            {
                Title = title,
                AuthorId = authorId,
                GenreId = genreId,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            }
        };

        var validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
    {
        var command = new UpdateBookCommand(null, null)
        {
            BookId = 1,
            Model = new UpdateBookModel()
            {
                Title = "Lord Of The Rings",
                AuthorId = 1,
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.Date
            }
        };

        var validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData("Lord Of The Rings", 1, 1, 100, 1)]
    [InlineData("Lordhe Rings", 1, 1, 100, 2)]
    [InlineData("Lord Of T", 2, 3, 100, 1)]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError(string title, int authorId, int genreId,
        int pageCount, int bookId)
    {
        var command = new UpdateBookCommand(null, null)
        {
            BookId = bookId,
            Model = new UpdateBookModel()
            {
                Title = title,
                AuthorId = authorId,
                GenreId = genreId,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            }
        };

        var validator = new UpdateBookCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}