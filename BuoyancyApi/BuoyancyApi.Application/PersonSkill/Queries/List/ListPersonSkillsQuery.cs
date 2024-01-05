using BuoyancyApi.Application.PersonSkills.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.PersonSkills.Queries.List;

public record ListPersonSkillsQuery : PaginatedListQuery
{

}

public class ListPersonSkillsQueryHandler : IRequestHandler<ListPersonSkillsQuery, PaginatedListResponse<PersonSkillDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ListPersonSkillsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PaginatedListResponse<PersonSkillDto>>> Handle(
        ListPersonSkillsQuery query,
        CancellationToken cancellationToken)
    {
        var personSkillQueryable = BuildQueryable(query);

        var result = await PaginatedListResponse<PersonSkillDto>.Create(
            personSkillQueryable,
            query,
            entity => PersonSkillDto.MapFromEntity(entity),
            cancellationToken);

        return new(200, result);
    }

    private IQueryable<PersonSkillEntity> BuildQueryable(ListPersonSkillsQuery query)
    {
        var queryable = _dbContext.PersonSkills;

        return queryable;
    }
}
