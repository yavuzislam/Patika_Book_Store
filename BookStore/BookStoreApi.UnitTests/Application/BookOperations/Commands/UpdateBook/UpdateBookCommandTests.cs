using AutoMapper;
using BookStore.Application.BookOperation.Commands.UpdateBook;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStoreApi.UnitTests.TestSetup;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public UpdateBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenBookIsNotFound_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        var command = new UpdateBookCommand(_context, _mapper)
        {
            BookId = 1
        };

        // act (Çalıştırma) & assert (Doğrulama)
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>()
            .And.Message.Should().Be("Book not found!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
    {
        var book = new Book()
        {
            Title = "Test_WhenValidInputsAreGiven_Book_ShouldBeUpdated",
            AuthorId = 1,
            GenreId = 1,
            PageCount = 100,
            PublishDate = new DateTime(1990, 01, 10)
        };
        _context.Books.Add(book);
        _context.SaveChanges();
        var command = new UpdateBookCommand(_context, _mapper)
        {
            BookId = 1,
            Model = new UpdateBookModel()
            {
                Title = "Test",
                AuthorId = 1,
                GenreId = 2,
                PageCount = 2,
                PublishDate = DateTime.Now.Date.AddYears(-1)
            }
        };
        _context.SaveChanges();

        FluentActions.Invoking(() => command.Handle()).Invoke();

        var bookToUpdate = _context.Books.SingleOrDefault(x => x.Id == command.BookId);
        bookToUpdate.Title.Should().Be(command.Model.Title);
        bookToUpdate.AuthorId.Should().Be(command.Model.AuthorId);
        bookToUpdate.GenreId.Should().Be(command.Model.GenreId);
        bookToUpdate.PageCount.Should().Be(command.Model.PageCount);
        bookToUpdate.PublishDate.Should().Be(command.Model.PublishDate.Date);
    }
}