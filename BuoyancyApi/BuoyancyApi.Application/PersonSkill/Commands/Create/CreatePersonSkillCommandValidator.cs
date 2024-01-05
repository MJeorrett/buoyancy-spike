
using FluentValidation;

namespace BuoyancyApi.Application.PersonSkills.Commands.Create;

public class CreatePersonSkillCommandValidator : AbstractValidator<CreatePersonSkillCommand>
{
    public CreatePersonSkillCommandValidator()
    {
        RuleFor(_ => _.PersonId)
            .NotNull();

        RuleFor(_ => _.SkillId)
            .NotNull();

    }
}