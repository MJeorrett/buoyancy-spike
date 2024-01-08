using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.PlannedTimes.Dtos;

public record ProjectPlannedTimeDto
{
    public int Id { get; init; }

    public required string ProjectTitle { get; init; }

    public List<ProjectWeekDto> Weeks { get; init; } = new();

    public static ProjectPlannedTimeDto MapFromEntity(ProjectEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            ProjectTitle = entity.Title,
            Weeks = entity.RequiredTime
                .Select(_ => new
                {
                    Planned = false,
                    WeekStartingMonday = _.WeekStartingMonday,
                    Hours = _.Hours,
                    RoleName = _.Role.Name,
                })
                .Concat(entity.PlannedTime.Select(_ => new
                {
                    Planned = true,
                    WeekStartingMonday = _.WeekStartingMonday,
                    Hours = _.Hours,
                    RoleName = _.Person.Role.Name,
                }))
                .GroupBy(_ => _.WeekStartingMonday)
                .Select(_ => new ProjectWeekDto
                {
                    WeekStartingMonday = _.Key,
                    TotalRequiredHours = _.Where(_ => !_.Planned).Sum(_ => _.Hours),
                    TotalPlannedHours = _.Where(_ => _.Planned).Sum(_ => _.Hours),
                    Time = _.GroupBy(_ => _.RoleName).Select(_ => new ProjectTimeEntryDto
                    {
                        RoleName = _.Key,
                        RequiredHours = _.Where(_ => !_.Planned).Sum(_ => _.Hours),
                        PlannedHours = _.Where(_ => _.Planned).Sum(_ => _.Hours),
                    }).ToList(),
                }).OrderBy(_ => _.WeekStartingMonday).ToList()
        };
    }
}

public record ProjectWeekDto
{
    public DateOnly WeekStartingMonday { get; init; }

    public decimal TotalRequiredHours { get; init; }
    public decimal TotalPlannedHours { get; init; }

    public List<ProjectTimeEntryDto> Time { get; init; } = new();
}

public record ProjectTimeEntryDto
{
    public int Id { get; init; }

    public required string RoleName { get; init; }

    public decimal RequiredHours { get; init; }

    public decimal PlannedHours { get; init; }
}
