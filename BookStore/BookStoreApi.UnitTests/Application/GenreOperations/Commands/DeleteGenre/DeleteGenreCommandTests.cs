using AutoMapper;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.DbOperations;
using BookStore.Entities;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.GenreOperations.Commands.DeleteGenre;

public class DeleteGenreCommandTests
{
    private readonly BookStoreDbContext _context;
    private IMapper _mapper;

    public DeleteGenreCommandTests(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [Fact]
    public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeThrown()
    {
        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.Id = 999;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Genre not found!");
    }
    
    [Fact]
    
    public void WhenValidInputsAreGiven_Genre_ShouldBeDeleted()
    {
        var genre = new Genre()
        {
            Name = "Test"
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();

        DeleteGenreCommand command = new DeleteGenreCommand(_context);
        command.Id = genre.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var deletedGenre = _context.Genres.SingleOrDefault(x => x.Id == genre.Id);
        deletedGenre.Should().BeNull();
    }
}