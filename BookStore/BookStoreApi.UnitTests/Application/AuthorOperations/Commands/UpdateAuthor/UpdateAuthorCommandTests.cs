using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DbOperations;
using BookStore.Entities;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandTests
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateAuthorCommandTests(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [Fact]
    public void WhenAlreadyExistingAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        var author = new Author()
        {
            Name = "Test",
        };

        _context.Authors.Add(author);
        _context.SaveChanges();

        var command = new UpdateAuthorCommand(_context, _mapper)
        {
            AuthorId = author.Id,
            Model = new UpdateAuthorModel()
            {
                Name = author.Name,
            }
        };
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Author already exists!");
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
    {
        var author = new Author()
        {
            Name = "Test"
        };
        _context.Authors.Add(author);
        _context.SaveChanges();
        var command = new UpdateAuthorCommand(_context, _mapper)
        {
            AuthorId = 1,
            Model = new UpdateAuthorModel()
            {
                Name = "Test"
            }
        };
        _context.SaveChanges();
        var updatedAuthor = _context.Authors.Find(1);
        updatedAuthor.Should().NotBeNull();
        updatedAuthor.Name.Should().Be(command.Model.Name);
    }
}