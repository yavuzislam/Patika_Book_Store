using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryValidatorTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(null)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
    {
        // arrange (Hazırlık)
        var query = new GetGenreDetailQuery(null, null)
        {
            GenreId = genreId
        };

        // act (Çalıştırma)
        var validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        // assert (Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int genreId)
    {
        // arrange (Hazırlık)
        var query = new GetGenreDetailQuery(null, null)
        {
            GenreId = genreId
        };

        // act (Çalıştırma)
        var validator = new GetGenreDetailQueryValidator();
        var result = validator.Validate(query);

        // assert (Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}