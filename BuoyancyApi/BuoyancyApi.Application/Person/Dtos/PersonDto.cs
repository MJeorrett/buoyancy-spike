using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.Persons.Dtos;

public record PersonDto
{
    public int Id { get; init; }

    public required string Name { get; set; }

    public int RoleId { get; set; }

    public required string RoleName { get; init; }

    public decimal WeeklyCapacityHours { get; init; }

    public List<PersonPlannedWeek> PlannedWeeks { get; init; } = new();


    public static PersonDto MapFromEntity(PersonEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            RoleId = entity.RoleId,
            RoleName = entity.Role.Name,
            WeeklyCapacityHours = entity.WeeklyCapacity,
            PlannedWeeks = entity.PlannedTime.GroupBy(_ => _.WeekStartingMonday).Select(_ => new PersonPlannedWeek
            {
                WeekStartingMonday = _.Key,
                TotalPlannedHours = _.Sum(_ => _.Hours),
            }).ToList()
        };
    }
}

public record PersonPlannedWeek
{
    public DateOnly WeekStartingMonday { get; init; }

    public decimal TotalPlannedHours { get; init; }
}
