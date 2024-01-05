namespace BuoyancyApi.E2eTests.Shared.Dtos.RequiredTimes;

public record CreateRequiredTimeDto
{
    public int Id { get; init; }
    public int ProjectId { get; init; }
    public int RoleId { get; init; }
    public int SkillId { get; init; }
    public int WeekStartingMonday { get; init; }
    public int Hours { get; init; }
}
