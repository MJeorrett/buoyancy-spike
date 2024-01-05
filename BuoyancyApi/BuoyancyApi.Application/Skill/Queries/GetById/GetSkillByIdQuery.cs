using BuoyancyApi.Application.Skills.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BuoyancyApi.Application.Skills.Queries.GetById;

public record GetSkillByIdQuery
{
    public int SkillId { get; init; }
}

public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, SkillDto>
{
    private readonly IApplicationDbContext _dbContext;

    public GetSkillByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<SkillDto>> Handle(
        GetSkillByIdQuery query,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Skills
            .FirstOrDefaultAsync(_ => _.Id == query.SkillId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        return new(200, SkillDto.MapFromEntity(entity));
    }
}
