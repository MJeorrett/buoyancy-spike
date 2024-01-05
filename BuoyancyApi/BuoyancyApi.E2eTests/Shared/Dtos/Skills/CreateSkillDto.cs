namespace BuoyancyApi.E2eTests.Shared.Dtos.Skills;

public record CreateSkillDto
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
}
