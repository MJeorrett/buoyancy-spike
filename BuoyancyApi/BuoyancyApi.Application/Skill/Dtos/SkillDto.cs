using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.Skills.Dtos;

public record SkillDto
{
    public int Id { get; init; }

    public string Name { get; set; } = default!;

    public static SkillDto MapFromEntity(SkillEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}
