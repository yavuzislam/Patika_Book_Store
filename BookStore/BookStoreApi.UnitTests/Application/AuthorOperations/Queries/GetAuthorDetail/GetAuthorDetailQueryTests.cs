using AutoMapper;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.DbOperations;
using BookStore.Entities;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryTests
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorDetailQueryTests(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [Fact]
    public void WhenAuthorIdIsInvalid_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        var query = new GetAuthorDetailQuery(_context, _mapper)
        {
            AuthorId = 1
        };
        // act (Çalıştırma) & assert (Doğrulama)
        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Author not found!");
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        var author = new Author()
        {
            Name = "Test_WhenValidInputsAreGiven_Author_ShouldBeReturn"
        };
        _context.Authors.Add(author);
        _context.SaveChanges();

        var query = new GetAuthorDetailQuery(_context, _mapper)
        {
            AuthorId = 1
        };

        // act (Çalıştırma)
        FluentActions.Invoking(() => query.Handle()).Invoke();

        // assert (Doğrulama)
        author = _context.Authors.SingleOrDefault(x => x.Id == query.AuthorId);
        author.Should().NotBeNull();
    }
}