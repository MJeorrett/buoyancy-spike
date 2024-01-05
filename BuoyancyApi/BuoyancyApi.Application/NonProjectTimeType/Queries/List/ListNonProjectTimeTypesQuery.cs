using BuoyancyApi.Application.NonProjectTimeTypes.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.NonProjectTimeTypes.Queries.List;

public record ListNonProjectTimeTypesQuery : PaginatedListQuery
{

}

public class ListNonProjectTimeTypesQueryHandler : IRequestHandler<ListNonProjectTimeTypesQuery, PaginatedListResponse<NonProjectTimeTypeDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ListNonProjectTimeTypesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PaginatedListResponse<NonProjectTimeTypeDto>>> Handle(
        ListNonProjectTimeTypesQuery query,
        CancellationToken cancellationToken)
    {
        var nonProjectTimeTypeQueryable = BuildQueryable(query);

        var result = await PaginatedListResponse<NonProjectTimeTypeDto>.Create(
            nonProjectTimeTypeQueryable,
            query,
            entity => NonProjectTimeTypeDto.MapFromEntity(entity),
            cancellationToken);

        return new(200, result);
    }

    private IQueryable<NonProjectTimeTypeEntity> BuildQueryable(ListNonProjectTimeTypesQuery query)
    {
        var queryable = _dbContext.NonProjectTimeTypes;

        return queryable;
    }
}
