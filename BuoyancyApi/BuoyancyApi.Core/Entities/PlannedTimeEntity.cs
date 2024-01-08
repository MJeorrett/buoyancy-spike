namespace BuoyancyApi.Core.Entities;

public class PlannedTimeEntity
{
    public int Id { get; set; }

    public int PersonId { get; set; }
    public PersonEntity Person { get; set; } = null!;

    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;

    public int? NonProjectTimeTypeId { get; set; }

    public DateOnly WeekStartingMonday { get; set; }

    public decimal Hours { get; set; }
}
