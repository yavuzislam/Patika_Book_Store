using FluentValidation;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator:AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(command => command.AuthorId).GreaterThan(0);
        RuleFor(command => command.Model.Name).MinimumLength(2).When(command => command.Model.Name.Trim() != string.Empty);
        RuleFor(command => command.Model.Surname).MinimumLength(2).When(command => command.Model.Surname.Trim() != string.Empty);
        RuleFor(command => command.Model.BirthDate.Date).NotEmpty().LessThan(DateTime.Now.Date);
    }
}