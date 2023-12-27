using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(command => command.Model.Name).MinimumLength(2);
        RuleFor(command => command.Model.Surname).MinimumLength(2);
        RuleFor(command => command.Model.BirthDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
    }
}