namespace BuoyancyApi.E2eTests.Shared.Dtos.Persons;

internal record UpdatePersonDto
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public int RoleId { get; init; }
}
