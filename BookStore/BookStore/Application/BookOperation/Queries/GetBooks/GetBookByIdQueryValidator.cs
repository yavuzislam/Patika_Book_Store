using FluentValidation;

namespace BookStore.Application.BookOperation.Queries.GetBooks;

public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {
        RuleFor(query => query.BookId).GreaterThan(0);
    }
}