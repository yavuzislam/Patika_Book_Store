using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.DbOperations;
using BookStore.Entities;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandTests
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateAuthorCommandTests(BookStoreDbContext context, IMapper mapper)
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

        var command = new CreateAuthorCommand(_context, _mapper)
        {
            Model = new CreateAuthorModel()
            {
                Name = author.Name,
            }
        };
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Author already exists!");
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
    {
        CreateAuthorModel model = new CreateAuthorModel()
        {
            Name = "Test"
        };
        CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper){Model = model};

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var author = _context.Authors.SingleOrDefault(x => x.Name == model.Name);
        author.Should().NotBeNull();
        author.Name.Should().Be(model.Name);
    }
}