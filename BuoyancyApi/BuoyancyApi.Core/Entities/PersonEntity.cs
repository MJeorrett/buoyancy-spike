namespace BuoyancyApi.Core.Entities;

public class PersonEntity
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public decimal WeeklyCapacity { get; set; }

    public int RoleId { get; set; }

    public RoleEntity Role { get; set; } = null!;

    public List<PlannedTimeEntity> PlannedTime { get; init; } = null!;
}
