
namespace FloatScheduleLoader.Dtos;

public record PaginatedListResponse<T>(
    List<T> Items,
    int TotalCount,
    int TotalPages,
    int PageIndex,
    int PageSize)
{
    public bool HasPreviousPage => PageIndex > 0;

    public bool HasNextPage => PageIndex < TotalPages - 1;
}
