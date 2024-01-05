namespace BuoyancyApi.Core.Entities;

public class PersonSkillEntity
{
    public int PersonId { get; set; }

    public PersonEntity Person { get; set; } = null!;

    public int SkillId { get; set; }

    public SkillEntity Skill { get; set; } = null!;
}
