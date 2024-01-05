
using FluentValidation;

namespace BuoyancyApi.Application.RequiredTimes.Commands.Create;

public class CreateRequiredTimeCommandValidator : AbstractValidator<CreateRequiredTimeCommand>
{
    public CreateRequiredTimeCommandValidator()
    {
        RuleFor(_ => _.ProjectId)
            .NotNull();

        RuleFor(_ => _.RoleId)
            .NotNull();

        RuleFor(_ => _.SkillId)
            .NotNull();

        RuleFor(_ => _.WeekStartingMonday)
            .NotNull();

        RuleFor(_ => _.Hours)
            .NotNull();

    }
}