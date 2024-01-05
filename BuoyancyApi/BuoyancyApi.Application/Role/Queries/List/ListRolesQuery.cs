using BuoyancyApi.Application.Roles.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.Roles.Queries.List;

public record ListRolesQuery : PaginatedListQuery
{

}

public class ListRolesQueryHandler : IRequestHandler<ListRolesQuery, PaginatedListResponse<RoleDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ListRolesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PaginatedListResponse<RoleDto>>> Handle(
        ListRolesQuery query,
        CancellationToken cancellationToken)
    {
        var roleQueryable = BuildQueryable(query);

        var result = await PaginatedListResponse<RoleDto>.Create(
            roleQueryable,
            query,
            entity => RoleDto.MapFromEntity(entity),
            cancellationToken);

        return new(200, result);
    }

    private IQueryable<RoleEntity> BuildQueryable(ListRolesQuery query)
    {
        var queryable = _dbContext.Roles;

        return queryable;
    }
}
