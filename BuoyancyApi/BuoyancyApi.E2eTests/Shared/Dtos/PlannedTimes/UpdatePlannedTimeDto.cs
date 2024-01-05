namespace BuoyancyApi.E2eTests.Shared.Dtos.PlannedTimes;

internal record UpdatePlannedTimeDto
{
    public int Id { get; init; }
    public int ProjectId { get; init; }
    public int PersonId { get; init; }
    public int? NonProjectTimeTypeId { get; init; }
    public int WeekStartingMonday { get; init; }
    public int Hours { get; init; }
}
