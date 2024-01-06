using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BuoyancyApi.Application.PlannedTimes.Commands.Update;

public record UpdatePlannedTimeCommand
{
    [JsonIgnore]
    public int PlannedTimeId { get; init; }
    public int ProjectId { get; init; }
    public int PersonId { get; init; }
    public int? NonProjectTimeTypeId { get; init; }
    public DateOnly WeekStartingMonday { get; init; }
    public decimal Hours { get; init; }
}

public class UpdatePlannedTimeCommandHandler : IRequestHandler<UpdatePlannedTimeCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdatePlannedTimeCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        UpdatePlannedTimeCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.PlannedTimes
            .FirstOrDefaultAsync(_ => _.Id == command.PlannedTimeId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        entity.ProjectId = command.ProjectId;
        entity.PersonId = command.PersonId;
        entity.NonProjectTimeTypeId = command.NonProjectTimeTypeId;
        entity.WeekStartingMonday = command.WeekStartingMonday;
        entity.Hours = command.Hours;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}