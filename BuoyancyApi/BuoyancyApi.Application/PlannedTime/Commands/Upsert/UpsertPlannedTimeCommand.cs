using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BuoyancyApi.Application.PlannedTime.Commands.Upsert;

public record UpsertPlannedTimeCommand
{
    [JsonIgnore]
    public int ProjectId { get; init; }

    public int PersonId { get; init; }

    public int? NonProjectTimeTypeId { get; init; }

    [JsonIgnore]
    public DateOnly WeekStartingMonday { get; init; }

    public decimal Hours { get; init; }
}

public class UpsertPlannedTimeCommandHandler : IRequestHandler<UpsertPlannedTimeCommand, int>
{
    private readonly IApplicationDbContext _dbContext;

    public UpsertPlannedTimeCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<int>> Handle(
        UpsertPlannedTimeCommand command,
        CancellationToken cancellationToken)
    {
        var existing = await _dbContext.PlannedTimes.FirstOrDefaultAsync(_ =>
            _.ProjectId == command.ProjectId &&
            _.PersonId == command.PersonId &&
            _.NonProjectTimeTypeId == command.NonProjectTimeTypeId &&
            _.WeekStartingMonday == command.WeekStartingMonday);

        if (existing is null)
        {
            return await Create(command, cancellationToken);
        }
        else
        {
            return await Update(command, existing, cancellationToken);
        }
    }

    private async Task<AppResponse<int>> Update(UpsertPlannedTimeCommand command, PlannedTimeEntity existing, CancellationToken cancellationToken)
    {
        existing.Hours = command.Hours;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(201, existing.Id);
    }

    private async Task<AppResponse<int>> Create(UpsertPlannedTimeCommand command, CancellationToken cancellationToken)
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
