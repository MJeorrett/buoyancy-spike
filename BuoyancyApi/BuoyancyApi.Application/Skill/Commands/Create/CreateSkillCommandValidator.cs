
using FluentValidation;

namespace BuoyancyApi.Application.Skills.Commands.Create;

public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
{
    public CreateSkillCommandValidator()
    {
        RuleFor(_ => _.Name)
            .NotNull()
            .NotEmpty();

    }
}