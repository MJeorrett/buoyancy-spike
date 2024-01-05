using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.PlannedTimes.Dtos;

public record PlannedTimeDto
{
    public int Id { get; init; }
    public int PersonId { get; set; } = default!;
    public int? ProjectId { get; set; }
    public int? NonProjectTimeTypeId { get; set; }
    public DateOnly WeekStartingMonday { get; set; } = default!;
    public int Hours { get; set; } = default!;

    public static PlannedTimeDto MapFromEntity(PlannedTimeEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            ProjectId = entity.ProjectId,
            PersonId = entity.PersonId,
            NonProjectTimeTypeId = entity.NonProjectTimeTypeId,
            WeekStartingMonday = entity.WeekStartingMonday,
            Hours = entity.Hours,
        };
    }
}
