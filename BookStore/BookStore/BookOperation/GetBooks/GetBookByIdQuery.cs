using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.BookOperation.GetBooks;

public class GetBookByIdQuery
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public int BookId { get; set; }

    public GetBookByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public GetBookViewModel Handle()
    {
        var book = _dbContext.Books.Find(BookId);
        if (book is null)
        {
            throw new InvalidOperationException("Book not found!");
        }

        var model = _mapper.Map<GetBookViewModel>(book);
        // GetBookViewModel model = new()
        // {
        //     Title = book.Title,
        //     Genre = ((GenreType)book.GenreId).ToString(),
        //     PageCount = book.PageCount,
        //     PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
        // };
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