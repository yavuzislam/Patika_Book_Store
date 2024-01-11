using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStoreApi.UnitTests.TestSetup;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateGenreCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistingGenreNameIsGiven_InvalidOperationException_ShouldBeThrown()
    {
        var genre = new Genre()
        {
            Name = "Romance"
        };

        _context.Genres.Add(genre);
        _context.SaveChanges();

        var command = new CreateGenreCommand(_context, _mapper)
        {
            Model = new CreateGenreModel()
            {
                Name = genre.Name
            }
        };
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Genre already exists!");
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
    {
        CreateGenreModel model = new CreateGenreModel()
        {
            Name = "Test"
        };
        CreateGenreCommand command = new CreateGenreCommand(_context, _mapper){Model = model};

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var genre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);
        genre.Should().NotBeNull();
        genre.Name.Should().Be(model.Name);
    }
}