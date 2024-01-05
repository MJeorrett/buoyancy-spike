namespace BuoyancyApi.Core.Entities;

public class RequiredTimeEntity
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public int RoleId { get; set; }

    public int SkillId { get; set; }

    public DateOnly WeekStartingMonday { get; set; }

    public int Hours { get; set; }
}
