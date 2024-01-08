using BuoyancyApi.Application.RequiredTimes.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuoyancyApi.Application.RequiredTimes.Queries.List;

public record ListRequiredTimesQuery : PaginatedListQuery
{

}

public class ListRequiredTimesQueryHandler : IRequestHandler<ListRequiredTimesQuery, PaginatedListResponse<ProjectRequiredTimeDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ListRequiredTimesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PaginatedListResponse<ProjectRequiredTimeDto>>> Handle(
        ListRequiredTimesQuery query,
        CancellationToken cancellationToken)
    {
        var requiredTimeQueryable = BuildQueryable(query);

        var result = await PaginatedListResponse<ProjectRequiredTimeDto>.Create(
            requiredTimeQueryable,
            query,
            entity => ProjectRequiredTimeDto.MapFromEntity(entity),
            cancellationToken);

        return new(200, result);
    }

    private IQueryable<ProjectEntity> BuildQueryable(ListRequiredTimesQuery query)
    {
        var queryable = _dbContext.Projects
            .Include(_ => _.RequiredTime.OrderBy(_ => _.WeekStartingMonday))
                .ThenInclude(_ => _.Role)
            .Where(_ => _.Title != "ClearSky Meetings");

        return queryable;
    }
}
