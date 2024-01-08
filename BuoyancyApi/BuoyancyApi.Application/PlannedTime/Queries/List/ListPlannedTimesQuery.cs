using BuoyancyApi.Application.PlannedTimes.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuoyancyApi.Application.PlannedTimes.Queries.List;

public record ListPlannedTimesQuery : PaginatedListQuery
{

}

public class ListPlannedTimesQueryHandler : IRequestHandler<ListPlannedTimesQuery, PaginatedListResponse<ProjectPlannedTimeDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ListPlannedTimesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PaginatedListResponse<ProjectPlannedTimeDto>>> Handle(
        ListPlannedTimesQuery query,
        CancellationToken cancellationToken)
    {
        var plannedTimeQueryable = BuildQueryable(query);

        var result = await PaginatedListResponse<ProjectPlannedTimeDto>.Create(
            plannedTimeQueryable,
            query,
            entity => ProjectPlannedTimeDto.MapFromEntity(entity),
            cancellationToken);

        return new(200, result);
    }

    private IQueryable<ProjectEntity> BuildQueryable(ListPlannedTimesQuery query)
    {
        var queryable = _dbContext.Projects
            .Include(_ => _.PlannedTime)
                .ThenInclude(_ => _.Person)
            .Include(_ => _.PlannedTime)
                .ThenInclude(_ => _.Person.Role);

        return queryable;
    }
}
