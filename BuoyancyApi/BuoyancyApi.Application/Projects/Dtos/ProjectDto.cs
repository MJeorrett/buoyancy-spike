using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.Projects.Dtos;

public record ProjectDto
{
    public int Id { get; init; }

    public string Title { get; init; } = "";

    public static ProjectDto MapFromEntity(ProjectEntity entity)
    {
        return new()
        {
            Id = entity.Id,
            Title = entity.Title,
        };
    }
}
