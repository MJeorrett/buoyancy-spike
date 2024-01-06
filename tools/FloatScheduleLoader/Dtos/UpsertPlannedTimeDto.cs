namespace FloatScheduleLoader.Dtos;

internal class UpsertPlannedTimeDto
{
    public required int PersonId { get; init; }

    public int? NonProjectTimeTypeId { get; init; }

    public required decimal Hours { get; init; }
}
