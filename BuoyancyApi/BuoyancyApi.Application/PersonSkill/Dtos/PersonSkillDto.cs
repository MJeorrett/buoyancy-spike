using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.PersonSkills.Dtos;

public record PersonSkillDto
{
    public int Id { get; init; }

    public int PersonId { get; set; } = default!;
    public int SkillId { get; set; } = default!;

    public static PersonSkillDto MapFromEntity(PersonSkillEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            PersonId = entity.PersonId,
            SkillId = entity.SkillId,
        };
    }
}
