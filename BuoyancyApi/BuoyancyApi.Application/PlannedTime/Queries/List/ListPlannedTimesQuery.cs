using BuoyancyApi.Application.PlannedTimes.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.PlannedTimes.Queries.List;

public record ListPlannedTimesQuery : PaginatedListQuery
{

}

public class ListPlannedTimesQueryHandler : IRequestHandler<ListPlannedTimesQuery, PaginatedListResponse<PlannedTimeDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ListPlannedTimesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PaginatedListResponse<PlannedTimeDto>>> Handle(
        ListPlannedTimesQuery query,
        CancellationToken cancellationToken)
    {
        var plannedTimeQueryable = BuildQueryable(query);

        var result = await PaginatedListResponse<PlannedTimeDto>.Create(
            plannedTimeQueryable,
            query,
            entity => PlannedTimeDto.MapFromEntity(entity),
            cancellationToken);

        return new(200, result);
    }

    private IQueryable<PlannedTimeEntity> BuildQueryable(ListPlannedTimesQuery query)
    {
        var queryable = _dbContext.PlannedTimes;

        return queryable;
    }
}
