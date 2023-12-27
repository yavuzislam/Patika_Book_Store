using AutoMapper;
using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookOperation.Queries.GetBooks;

public class GetBookByIdQuery
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public int BookId { get; set; }

    public GetBookByIdQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public GetBookViewModel Handle()
    {
        var book = _dbContext.Books.Include(x => x.Genre).SingleOrDefault(x => x.Id == BookId);
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
    
    public string Author { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
}