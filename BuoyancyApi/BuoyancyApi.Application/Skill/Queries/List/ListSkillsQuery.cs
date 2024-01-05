using BuoyancyApi.Application.Skills.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.Skills.Queries.List;

public record ListSkillsQuery : PaginatedListQuery
{

}

public class ListSkillsQueryHandler : IRequestHandler<ListSkillsQuery, PaginatedListResponse<SkillDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ListSkillsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PaginatedListResponse<SkillDto>>> Handle(
        ListSkillsQuery query,
        CancellationToken cancellationToken)
    {
        var skillQueryable = BuildQueryable(query);

        var result = await PaginatedListResponse<SkillDto>.Create(
            skillQueryable,
            query,
            entity => SkillDto.MapFromEntity(entity),
            cancellationToken);

        return new(200, result);
    }

    private IQueryable<SkillEntity> BuildQueryable(ListSkillsQuery query)
    {
        var queryable = _dbContext.Skills;

        return queryable;
    }
}
