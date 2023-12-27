using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.BookOperation.Commands.UpdateBook;

public class UpdateBookCommand
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public UpdateBookModel Model { get; set; }
    public int BookId { get; set; }

    public UpdateBookCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
        if (book is null)
        {
            throw new InvalidOperationException("Book not found!");
        }

        _mapper.Map(Model, book);
        // book.Title = Model.Title != default ? Model.Title : book.Title;
        // book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
        // book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;
        // book.PublishDate = Model.PublishDate != default ? Model.PublishDate.Date : book.PublishDate.Date;

        _dbContext.SaveChanges();
    }
}

public class UpdateBookModel
{
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}