using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.NonProjectTimeTypes.Dtos;

public record NonProjectTimeTypeDto
{
    public int Id { get; init; }

    public string Name { get; set; } = default!;

    public static NonProjectTimeTypeDto MapFromEntity(NonProjectTimeTypeEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}
