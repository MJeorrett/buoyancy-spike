
using FluentValidation;

namespace BuoyancyApi.Application.Roles.Commands.Create;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(_ => _.Name)
            .NotNull()
            .NotEmpty();

    }
}