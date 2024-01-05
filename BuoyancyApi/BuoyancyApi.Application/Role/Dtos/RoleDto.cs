using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.Roles.Dtos;

public record RoleDto
{
    public int Id { get; init; }

    public string Name { get; set; } = default!;

    public static RoleDto MapFromEntity(RoleEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }
}
