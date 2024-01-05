namespace BuoyancyApi.Core.Entities;

public class PersonSkillEntity
{
    // This should have a composite PK but keeping simple as possible for spike.
    public int Id { get; set; }

    public int PersonId { get; set; }

    public PersonEntity Person { get; set; } = null!;

    public int SkillId { get; set; }

    public SkillEntity Skill { get; set; } = null!;
}
