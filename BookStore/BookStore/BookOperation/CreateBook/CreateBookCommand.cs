using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.BookOperation.CreateBook;

public class CreateBookCommand
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateBookModel Model { get; set; }

    public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
        if (book is not null)
        {
            throw new InvalidOperationException("Book already exists!");
        }

        book = _mapper.Map<Book>(Model);

        // book = new Book() 
        // {
        //     Title = Model.Title,
        //     GenreId = Model.GenreId,
        //     PageCount = Model.PageCount,
        //     PublishDate = Model.PublishDate.Date
        // };

        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }
}

public class CreateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}