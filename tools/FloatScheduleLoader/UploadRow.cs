namespace FloatScheduleLoader;

internal record UploadRow
{
    public required string Name { get; init; }

    public required string Project { get; init; }

    public required string? TimeOff { get; init; }

    public Dictionary<DateOnly, decimal> WeeklyHours { get; init; } = new();
}
