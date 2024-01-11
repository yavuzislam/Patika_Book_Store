using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.DbOperations;
using BookStore.Entities;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandTests
{
    private readonly BookStoreDbContext _context;

    public DeleteAuthorCommandTests(BookStoreDbContext context)
    {
        _context = context;
    }
    
    [Fact]
    public void WhenInvalidIdIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.AuthorId = 999;

        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Author not found!");
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Author_ShouldBeDeleted()
    {
        var author = new Author()
        {
            Name = "Test"
        };
        _context.Authors.Add(author);
        _context.SaveChanges();

        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.AuthorId = author.Id;

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var deletedAuthor = _context.Authors.SingleOrDefault(x => x.Id == author.Id);
        deletedAuthor.Should().BeNull();
    }
}