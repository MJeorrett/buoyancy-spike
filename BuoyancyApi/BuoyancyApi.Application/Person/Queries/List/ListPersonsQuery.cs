using BuoyancyApi.Application.Persons.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.Persons.Queries.List;

public record ListPersonsQuery : PaginatedListQuery
{

}

public class ListPersonsQueryHandler : IRequestHandler<ListPersonsQuery, PaginatedListResponse<PersonDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ListPersonsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PaginatedListResponse<PersonDto>>> Handle(
        ListPersonsQuery query,
        CancellationToken cancellationToken)
    {
        var personQueryable = BuildQueryable(query);

        var result = await PaginatedListResponse<PersonDto>.Create(
            personQueryable,
            query,
            entity => PersonDto.MapFromEntity(entity),
            cancellationToken);

        return new(200, result);
    }

    private IQueryable<PersonEntity> BuildQueryable(ListPersonsQuery query)
    {
        var queryable = _dbContext.Persons;

        return queryable;
    }
}
