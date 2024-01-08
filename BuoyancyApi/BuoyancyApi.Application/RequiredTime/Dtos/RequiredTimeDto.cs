using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.RequiredTimes.Dtos;

public record ProjectRequiredTimeDto
{
    public int Id { get; init; }

    public required string ProjectTitle { get; init; }

    public List<ProjectRequiredWeekDto> Weeks { get; set; } = new();

    public static ProjectRequiredTimeDto MapFromEntity(ProjectEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            ProjectTitle = entity.Title,
            Weeks = entity.RequiredTime.GroupBy(_ => _.WeekStartingMonday)
                .Select(_ => new ProjectRequiredWeekDto
                {
                    WeekStartingMonday = _.Key,
                    TotalHours = _.Sum(_ => _.Hours),
                    Entries = _.GroupBy(_ => _.Role.Name).Select(_ => new ProjectRequiredTimeEntryDto
                    {
                        RoleName = _.Key,
                        Hours = _.Sum(_ => _.Hours),
                    }).ToList(),
                }).ToList()
        };
    }
}

public record ProjectRequiredWeekDto
{
    public DateOnly WeekStartingMonday { get; init; }

    public decimal TotalHours { get; init; }

    public List<ProjectRequiredTimeEntryDto> Entries { get; init; } = new();
}

public record ProjectRequiredTimeEntryDto
{
    public int Id { get; init; }

    public required string RoleName { get; init; }

    public decimal Hours { get; init; }
}
