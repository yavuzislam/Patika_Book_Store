using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryValidatorTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(null)]
    public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId)
    {
        // arrange (Hazırlık)
        var query = new GetAuthorDetailQuery(null, null)
        {
            AuthorId = authorId
        };

        // act (Çalıştırma)
        var validator = new GetAuthorDetailQueryValidator();
        var result = validator.Validate(query);

        // assert (Doğrulama)
        result.Errors.Count.Should().BeGreaterThan(0);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnErrors(int authorId)
    {
        // arrange (Hazırlık)
        var query = new GetAuthorDetailQuery(null, null)
        {
            AuthorId = authorId
        };

        // act (Çalıştırma)
        var validator = new GetAuthorDetailQueryValidator();
        var result = validator.Validate(query);

        // assert (Doğrulama)
        result.Errors.Count.Should().Be(0);
    }
}