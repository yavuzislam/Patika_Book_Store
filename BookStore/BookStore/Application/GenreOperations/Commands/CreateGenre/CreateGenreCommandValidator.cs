using FluentValidation;

namespace BookStore.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommandValidator:AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(command => command.Model.Name).MinimumLength(2);
    }
}