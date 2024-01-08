using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.PlannedTimes.Dtos;

public record ProjectPlannedTimeDto
{
    public int Id { get; init; }

    public required string ProjectTitle { get; init; }

    public List<ProjectPlannedWeekDto> Weeks { get; init; } = new();

    public static ProjectPlannedTimeDto MapFromEntity(ProjectEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            ProjectTitle = entity.Title,
            Weeks = entity.PlannedTime.GroupBy(_ => _.WeekStartingMonday)
                .Select(_ => new ProjectPlannedWeekDto
                {
                    WeekStartingMonday = _.Key,
                    Entries = _.Select(_ => new ProjectPlannedTimeEntryDto
                    {
                        Id = _.Id,
                        PersonId = _.PersonId,
                        PersonName = _.Person.Name,
                        RoleName = _.Person.Role.Name,
                        Hours = _.Hours,
                    }).ToList(),
                }).ToList()
        };
    }
}

public record ProjectPlannedWeekDto
{
    public DateOnly WeekStartingMonday { get; init; }

    public List<ProjectPlannedTimeEntryDto> Entries { get; init; } = new();
}

public record ProjectPlannedTimeEntryDto
{
    public int Id { get; init; }

    public int PersonId { get; init; }

    public required string PersonName { get; init; }

    public required string RoleName { get; init; }

    public decimal Hours { get; init; }
}
