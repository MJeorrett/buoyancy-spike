namespace FloatScheduleLoader.Dtos;

public record PaginatedListQuery
{
    public int PageIndex { get; init; }
    public int PageSize { get; init; }

    public string ToQueryString()
    {
        return $"pageIndex={PageIndex}&pageSize={PageSize}";
    }
}
