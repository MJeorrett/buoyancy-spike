namespace BuoyancyApi.E2eTests.Shared.Dtos.PersonSkills;

public record PersonSkillDto
{
    public int Id { get; init; }

    public int PersonId { get; init; }
    public int SkillId { get; init; }
}