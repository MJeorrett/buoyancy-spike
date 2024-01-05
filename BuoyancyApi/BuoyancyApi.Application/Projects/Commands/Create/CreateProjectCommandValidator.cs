using FluentValidation;

namespace BuoyancyApi.Application.Projects.Commands.Create;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(_ => _.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(256);
    }
}
