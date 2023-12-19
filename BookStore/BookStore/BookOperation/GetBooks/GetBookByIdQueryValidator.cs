using FluentValidation;

namespace BookStore.BookOperation.GetBooks;

public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {
        RuleFor(query => query.BookId).GreaterThan(0);
    }
}