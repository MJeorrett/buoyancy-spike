using BuoyancyApi.Application.RequiredTimes.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.RequiredTimes.Queries.List;

public record ListRequiredTimesQuery : PaginatedListQuery
{

}

public class ListRequiredTimesQueryHandler : IRequestHandler<ListRequiredTimesQuery, PaginatedListResponse<RequiredTimeDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ListRequiredTimesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PaginatedListResponse<RequiredTimeDto>>> Handle(
        ListRequiredTimesQuery query,
        CancellationToken cancellationToken)
    {
        var requiredTimeQueryable = BuildQueryable(query);

        var result = await PaginatedListResponse<RequiredTimeDto>.Create(
            requiredTimeQueryable,
            query,
            entity => RequiredTimeDto.MapFromEntity(entity),
            cancellationToken);

        return new(200, result);
    }

    private IQueryable<RequiredTimeEntity> BuildQueryable(ListRequiredTimesQuery query)
    {
        var queryable = _dbContext.RequiredTimes;

        return queryable;
    }
}
