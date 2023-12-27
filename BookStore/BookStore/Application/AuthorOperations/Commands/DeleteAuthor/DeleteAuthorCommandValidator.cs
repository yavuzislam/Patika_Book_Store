using BookStore.DbOperations;
using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor;

public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
{
    private readonly IBookStoreDbContext _dbContext;

    public DeleteAuthorCommandValidator(IBookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(command => command.AuthorId).GreaterThan(0);
        // RuleFor(command => command.AuthorId).Must(authorId =>
        // {
        //     return !_dbContext.Books.Any(x => x.AuthorId == authorId);
        // }).WithMessage("Author has active books. Therefore, cannot be deleted.");
    }
}