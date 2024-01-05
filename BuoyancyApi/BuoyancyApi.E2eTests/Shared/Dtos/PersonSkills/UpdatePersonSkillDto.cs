namespace BuoyancyApi.E2eTests.Shared.Dtos.PersonSkills;

internal record UpdatePersonSkillDto
{
    public int Id { get; init; }
    public int PersonId { get; init; }
    public int SkillId { get; init; }
}
