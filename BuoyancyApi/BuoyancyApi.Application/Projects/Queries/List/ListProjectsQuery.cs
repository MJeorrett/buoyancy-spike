using BuoyancyApi.Application.Projects.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuoyancyApi.Application.Projects.Queries.List;

public record ListProjectsQuery : PaginatedListQuery
{

}

public class ListProjectsQueryHandler : IRequestHandler<ListProjectsQuery, PaginatedListResponse<ProjectDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ListProjectsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PaginatedListResponse<ProjectDto>>> Handle(
        ListProjectsQuery query,
        CancellationToken cancellationToken)
    {
        var projectQueryable = BuildQueryable(query);

        var result = await PaginatedListResponse<ProjectDto>.Create(
            projectQueryable,
            query,
            entity => ProjectDto.MapFromEntity(entity),
            cancellationToken);

        return new(200, result);
    }

    private IQueryable<ProjectEntity> BuildQueryable(ListProjectsQuery query)
    {
        var queryable = _dbContext.Projects;

        return queryable;
    }
}
