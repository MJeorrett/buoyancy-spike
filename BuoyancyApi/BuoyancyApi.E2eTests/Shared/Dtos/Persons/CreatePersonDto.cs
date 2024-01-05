namespace BuoyancyApi.E2eTests.Shared.Dtos.Persons;

public record CreatePersonDto
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public int RoleId { get; init; }
}
