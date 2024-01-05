namespace BuoyancyApi.E2eTests.Shared.Dtos.Roles;

public record CreateRoleDto
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
}
