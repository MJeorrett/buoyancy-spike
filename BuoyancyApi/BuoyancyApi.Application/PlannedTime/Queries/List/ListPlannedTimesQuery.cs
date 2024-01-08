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

        var allDates = result.Items.SelectMany(_ => _.Weeks).Select(_ => _.WeekStartingMonday).Distinct().ToList();

        foreach (var r in result.Items)
        {
            foreach (var date in allDates)
            {
                if (!r.Weeks.Any(_ => _.WeekStartingMonday == date))
                {
                    r.Weeks.Add(new()
                    {
                        WeekStartingMonday = date,
                        TotalRequiredHours = 0,
                        TotalPlannedHours = 0,
                        Time = new(),
                    });
                }
            }

            r.Weeks = r.Weeks.OrderBy(_ => _.WeekStartingMonday).ToList();
        }

        return new(200, result);
    }

    private IQueryable<ProjectEntity> BuildQueryable(ListPlannedTimesQuery query)
    {
        var queryable = _dbContext.Projects
            .Include(_ => _.PlannedTime.OrderBy(_ => _.WeekStartingMonday))
                .ThenInclude(_ => _.Person.Role)
            .Include(_ => _.RequiredTime.OrderBy(_ => _.WeekStartingMonday))
                .ThenInclude(_ => _.Role)
            .Where(_ => _.Title != "ClearSky Meetings")
            .OrderBy(_ => _.Title);

        return queryable;
    }
}
