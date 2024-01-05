using BuoyancyApi.Application.Projects.Commands.Create;
using FluentValidation;

namespace BuoyancyApi.Application.Projects.Commands.Update;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(_ => _.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(256);
    }
}
