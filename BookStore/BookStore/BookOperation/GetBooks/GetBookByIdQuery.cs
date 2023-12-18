using BookStore.Common;
using BookStore.DbOperations;

namespace BookStore.BookOperation.GetBooks;

public class GetBookByIdQuery
{
    private readonly BookStoreDbContext _dbContext;

    public int BookId { get; set; }

    public GetBookByIdQuery(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public GetBookViewModel Handle()
    {
        var book = _dbContext.Books.Find(BookId);
        if (book is null)
        {
            throw new InvalidOperationException("Book not found!");
        }

        GetBookViewModel model = new()
        {
            Title = book.Title,
            Genre = ((GenreType)book.GenreId).ToString(),
            PageCount = book.PageCount,
            PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
        };
        return model;
    }
}

public class GetBookViewModel
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
}