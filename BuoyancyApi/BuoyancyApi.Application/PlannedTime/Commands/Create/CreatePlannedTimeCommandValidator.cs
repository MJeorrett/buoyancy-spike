
using FluentValidation;

namespace BuoyancyApi.Application.PlannedTimes.Commands.Create;

public class CreatePlannedTimeCommandValidator : AbstractValidator<CreatePlannedTimeCommand>
{
    public CreatePlannedTimeCommandValidator()
    {
        RuleFor(_ => _.ProjectId)
            .NotNull();

        RuleFor(_ => _.PersonId)
            .NotNull();

        

        RuleFor(_ => _.WeekStartingMonday)
            .NotNull();

        RuleFor(_ => _.Hours)
            .NotNull();

    }
}