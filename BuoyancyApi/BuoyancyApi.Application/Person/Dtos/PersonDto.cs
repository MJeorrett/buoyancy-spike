using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.Persons.Dtos;

public record PersonDto
{
    public int Id { get; init; }

    public string Name { get; set; } = default!;
    public int RoleId { get; set; } = default!;

    public static PersonDto MapFromEntity(PersonEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
            RoleId = entity.RoleId,
        };
    }
}
