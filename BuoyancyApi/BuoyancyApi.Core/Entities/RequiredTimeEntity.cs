namespace BuoyancyApi.Core.Entities;

public class RequiredTimeEntity
{
    public int Id { get; set; }

    public int ProjectId { get; set; }
    public ProjectEntity Project { get; set; } = null!;

    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;

    public int SkillId { get; set; }

    public DateOnly WeekStartingMonday { get; set; }

    public decimal Hours { get; set; }
}
