using AutoMapper;
using BookStore.Application.BookOperation.Queries.GetBooks;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStoreApi.UnitTests.TestSetup;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.BookOperations.Queries.GetBookDetail;

public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetBookDetailQueryTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(1000)]
    public void WhenBookIdIsInvalid_InvalidOperationException_ShouldBeReturn(int bookId)
    {
        // arrange (Hazırlık)
        var query = new GetBookByIdQuery(_context, _mapper)
        {
            BookId = bookId
        };
        // act (Çalıştırma) & assert (Doğrulama)
        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Book not found!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        var book = new Book()
        {
            Title = "Test_WhenValidInputsAreGiven_Book_ShouldBeReturn",
            AuthorId = 1,
            GenreId = 1,
            PageCount = 100,
            PublishDate = new DateTime(1990, 01, 10)
        };
        _context.Books.Add(book);
        _context.SaveChanges();

        var query = new GetBookByIdQuery(_context, _mapper)
        {
            BookId = 1
        };

        // act (Çalıştırma)
        FluentActions.Invoking(() => query.Handle()).Invoke();

        // assert (Doğrulama)
        book = _context.Books.SingleOrDefault(x => x.Id == query.BookId);
        book.Should().NotBeNull();
    }
}