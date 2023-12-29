using AutoMapper;
using BookStore.Application.BookOperation.Commands.CreateBook;
using BookStore.DbOperations;
using BookStore.Entities;
using BookStoreApi.UnitTests.TestSetup;
using FluentAssertions;

namespace BookStoreApi.UnitTests.Application.BookOperations.Commands.CreateBook;

public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public CreateBookCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
    {
        // arrange (Hazırlık)
        var book = new Book()
        {
            Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
            AuthorId = 1,
            GenreId = 1,
            PageCount = 100,
            PublishDate = new DateTime(1990, 01, 10)
        };
        _context.Books.Add(book);
        _context.SaveChanges();

        CreateBookCommand command = new CreateBookCommand(_context, _mapper);
        command.Model = new CreateBookModel()
        {
            Title = book.Title,
        };

        // act (Çalıştırma) & assert (Doğrulama)
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should()
            .Be("Book already exists!");
    }

    [Fact]
    public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
    {
        // arrange (Hazırlık)
        CreateBookModel model = new CreateBookModel()
        {
            Title = "Test",
            AuthorId = 1,
            GenreId = 1,
            PageCount = 100,
            PublishDate = DateTime.Now.Date.AddYears(-1)
        };
        CreateBookCommand command = new CreateBookCommand(_context, _mapper){Model = model};

        // act (Çalıştırma)
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // assert (Doğrulama)
        var book = _context.Books.SingleOrDefault(x => x.Title == model.Title);
        book.Should().NotBeNull();
        book.PageCount.Should().Be(model.PageCount);
        book.PublishDate.Should().Be(model.PublishDate.Date);
        book.GenreId.Should().Be(model.GenreId);
        book.AuthorId.Should().Be(model.AuthorId);
    }
}