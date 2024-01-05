namespace BuoyancyApi.Core.Entities;

public class PlannedTimeEntity
{
    public int Id { get; set; }

    public int PersonId { get; set; }

    public int ProjectId { get; set; }

    public int? NonProjectTimeTypeId { get; set; }

    public DateOnly WeekStartingMonday { get; set; }

    public int Hours { get; set; }
}
