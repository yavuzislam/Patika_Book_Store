using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommand
{
    private readonly IBookStoreDbContext _dbContext;
    public int AuthorId { get; set; }

    public DeleteAuthorCommand(IBookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
        if (author is null)
            throw new InvalidOperationException("Author not found.");
        var book = _dbContext.Books.SingleOrDefault(x => x.AuthorId == AuthorId);
        if (book is not null)
            throw new InvalidOperationException("Author has active books. Therefore, cannot be deleted.");

        _dbContext.Authors.Remove(author);
        _dbContext.SaveChanges();
    }
}