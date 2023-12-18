using BookStore.DbOperations;

namespace BookStore.BookOperation.UpdateBook;

public class UpdateBookCommand
{
    private readonly BookStoreDbContext _dbContext;
    public UpdateBookModel Model { get; set; }
    public int BookId { get; set; }

    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
        if (book is null)
        {
            throw new InvalidOperationException("Book not found!");
        }

        book.Title = Model.Title != default ? Model.Title : book.Title;
        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
        book.PublishDate = Model.PublishDate != default ? Model.PublishDate.Date : book.PublishDate.Date;

        _dbContext.SaveChanges();
    }
}

public class UpdateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}