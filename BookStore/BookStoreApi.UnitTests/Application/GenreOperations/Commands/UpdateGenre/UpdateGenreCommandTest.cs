using AutoMapper;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.DbOperations;
using BookStore.Entities;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandTest
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateGenreCommandTest(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [Fact]
    public void WhenGenreIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        var command = new UpdateGenreCommand(_context, _mapper)
        {
            GenreId = 1
        };

        // act (Çalıştırma) & assert (Doğrulama)
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Genre not found!");
    }
    
    [Fact]
public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
    {
        var genre = new Genre()
        {
            Name = "Test_WhenValidInputsAreGiven_Genre_ShouldBeUpdated"
        };
        _context.Genres.Add(genre);
        _context.SaveChanges();
        var command = new UpdateGenreCommand(_context, _mapper)
        {
            GenreId = 1,
            Model = new UpdateGenreModel()
            {
                Name = "Test"
            }
        };
        _context.SaveChanges();
        var updatedGenre = _context.Genres.Find(1);
        updatedGenre.Should().NotBeNull();
        updatedGenre.Name.Should().Be(command.Model.Name);
    }
    
}