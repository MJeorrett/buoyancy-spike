using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.RequiredTimes.Dtos;

public record RequiredTimeDto
{
    public int Id { get; init; }

    public int ProjectId { get; set; } = default!;
    public int RoleId { get; set; } = default!;
    public int SkillId { get; set; } = default!;
    public DateOnly WeekStartingMonday { get; set; } = default!;
    public decimal Hours { get; set; } = default!;

    public static RequiredTimeDto MapFromEntity(RequiredTimeEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            ProjectId = entity.ProjectId,
            RoleId = entity.RoleId,
            SkillId = entity.SkillId,
            WeekStartingMonday = entity.WeekStartingMonday,
            Hours = entity.Hours,
        };
    }
}
