using BookStore.Application.BookOperation.Commands.DeleteBook;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStoreApi.UnitTests.TestSetup;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.BookOperations.Commands.DeleteBook;

public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;

    public DeleteBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
    }

    [Fact]
    public void WhenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        DeleteBookCommand command = new DeleteBookCommand(_context);
        command.BookId = 1;

        // act (Çalıştırma) & assert (Doğrulama)
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Book not found!");
    }
    
    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeDeleted()
    {
        // arrange (Hazırlık)
        DeleteBookCommand command = new DeleteBookCommand(_context) { BookId = 1};
        Book book = new Book()
        {
            Title = "Test",
            AuthorId = 1,
            GenreId = 1,
            PageCount = 100,
            PublishDate = DateTime.Now.Date.AddYears(-1)
        };
        _context.Books.Add(book);
        _context.SaveChanges();

        // act (Çalıştırma)
        FluentActions.Invoking(() => command.Handle()).Invoke();
        
        // assert (Doğrulama)
        var bookToDelete = _context.Books.SingleOrDefault(x => x.Id == command.BookId);
        bookToDelete.Should().BeNull();
    }
}