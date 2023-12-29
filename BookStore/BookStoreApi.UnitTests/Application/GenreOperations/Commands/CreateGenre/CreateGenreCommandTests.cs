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
}