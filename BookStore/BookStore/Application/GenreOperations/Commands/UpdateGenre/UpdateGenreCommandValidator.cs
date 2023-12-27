﻿using FluentValidation;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(command => command.GenreId).GreaterThan(0);
        RuleFor(command => command.Model.Name).MinimumLength(3).When(x => x.Model.Name.Trim() != string.Empty);
        // RuleFor(command=>command.Model.Name).MinimumLength(4).When(command=>command.Model.Name!=default);
    }
}