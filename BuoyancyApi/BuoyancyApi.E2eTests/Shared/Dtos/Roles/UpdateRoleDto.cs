namespace BuoyancyApi.E2eTests.Shared.Dtos.Roles;

internal record UpdateRoleDto
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
}
