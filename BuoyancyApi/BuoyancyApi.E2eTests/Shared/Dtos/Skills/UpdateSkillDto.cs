namespace BuoyancyApi.E2eTests.Shared.Dtos.Skills;

internal record UpdateSkillDto
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
}
