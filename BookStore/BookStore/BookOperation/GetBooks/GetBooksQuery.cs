using AutoMapper;
using BookStore.Common;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.BookOperation.GetBooks;

public class GetBooksQuery
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<BooksViewModel> Handle()
    {
        var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList();
        var vm = _mapper.Map<List<BooksViewModel>>(bookList);
        // var vm = bookList.Select(book => new BooksViewModel()
        // {
        //     Title = book.Title, Genre = ((GenreType)book.GenreId).ToString(), PageCount = book.PageCount,
        //     PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy")
        // }).ToList();

        return vm;
    }
}

public class BooksViewModel
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PageCount { get; set; }
    public string PublishDate { get; set; }
}