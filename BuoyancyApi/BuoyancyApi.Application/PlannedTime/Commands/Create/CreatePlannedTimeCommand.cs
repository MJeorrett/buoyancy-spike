using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.PlannedTimes.Commands.Create;

public class CreatePlannedTimeCommand
{
    public int ProjectId { get; init; }
    public int PersonId { get; init; }
    public int? NonProjectTimeTypeId { get; init; }
    public DateOnly WeekStartingMonday { get; init; }
    public int Hours { get; init; }
}

public class CreatePlannedTimeCommandHandler : IRequestHandler<CreatePlannedTimeCommand, int>
{
    private readonly IApplicationDbContext _dbContext;

    public CreatePlannedTimeCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<int>> Handle(
        CreatePlannedTimeCommand command,
        CancellationToken cancellationToken)
    {
        var entity = new PlannedTimeEntity
        {
            ProjectId = command.ProjectId,
            PersonId = command.PersonId,
            NonProjectTimeTypeId = command.NonProjectTimeTypeId,
            WeekStartingMonday = command.WeekStartingMonday,
            Hours = command.Hours,
        };

        _dbContext.PlannedTimes.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(201, entity.Id);
    }
}