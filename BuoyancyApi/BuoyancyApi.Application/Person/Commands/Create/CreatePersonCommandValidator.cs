
using FluentValidation;

namespace BuoyancyApi.Application.Persons.Commands.Create;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(_ => _.Name)
            .NotNull()
            .NotEmpty();

        RuleFor(_ => _.RoleId)
            .NotNull();

    }
}