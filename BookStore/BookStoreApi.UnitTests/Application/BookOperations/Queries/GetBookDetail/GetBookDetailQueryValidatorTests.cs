using BookStore.Application.BookOperation.Queries.GetBooks;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryValidatorTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(null)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
    {
        // arrange (Hazırlık)
        var query = new GetBookByIdQuery(null, null)
        {
            BookId = bookId
        };

        // act (Çalıştırma)
        var validator = new GetBookByIdQueryValidator();
        var result = validator.Validate(query);

        // assert (Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int bookId)
    {
        // arrange (Hazırlık)
        var query = new GetBookByIdQuery(null, null)
        {
            BookId = bookId
        };

        // act (Çalıştırma)
        var validator = new GetBookByIdQueryValidator();
        var result = validator.Validate(query);

        // assert (Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}