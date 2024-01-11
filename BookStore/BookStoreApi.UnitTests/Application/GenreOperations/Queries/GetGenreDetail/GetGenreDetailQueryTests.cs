using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.DbOperations;
using BookStore.Entities;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQueryTests
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenreDetailQueryTests(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(1000)]
    public void WhenGenreIdIsInvalid_InvalidOperationException_ShouldBeReturn(int genreId)
    {
        // arrange (Hazırlık)
        var query = new GetGenreDetailQuery(_context, _mapper)
        {
            GenreId = genreId
        };
        // act (Çalıştırma) & assert (Doğrulama)
        FluentActions.Invoking(() => query.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Genre not found!");
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        var genre = new Genre()
        {
            Name = "Test_WhenValidInputsAreGiven_Genre_ShouldBeReturn"
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();

        var query = new GetGenreDetailQuery(_context, _mapper)
        {
            GenreId = 1
        };

        // act (Çalıştırma)
        FluentActions.Invoking(() => query.Handle()).Invoke();

        // assert (Doğrulama)
        genre = _context.Genres.SingleOrDefault(x => x.Id == query.GenreId);
        genre.Should().NotBeNull();
    }
}