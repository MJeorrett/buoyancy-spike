using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BuoyancyApi.Application.RequiredTimes.Commands.Update;

public record UpdateRequiredTimeCommand
{
    [JsonIgnore]
    public int RequiredTimeId { get; init; }
    public int ProjectId { get; init; }
    public int RoleId { get; init; }
    public int SkillId { get; init; }
    public DateOnly WeekStartingMonday { get; init; }
    public decimal Hours { get; init; }
}

public class UpdateRequiredTimeCommandHandler : IRequestHandler<UpdateRequiredTimeCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateRequiredTimeCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        UpdateRequiredTimeCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.RequiredTimes
            .FirstOrDefaultAsync(_ => _.Id == command.RequiredTimeId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        entity.ProjectId = command.ProjectId;
        entity.RoleId = command.RoleId;
        entity.SkillId = command.SkillId;
        entity.WeekStartingMonday = command.WeekStartingMonday;
        entity.Hours = command.Hours;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}