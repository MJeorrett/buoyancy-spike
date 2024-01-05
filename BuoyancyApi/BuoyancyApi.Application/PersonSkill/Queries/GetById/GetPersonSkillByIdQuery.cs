using BuoyancyApi.Application.PersonSkills.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BuoyancyApi.Application.PersonSkills.Queries.GetById;

public record GetPersonSkillByIdQuery
{
    public int PersonSkillId { get; init; }
}

public class GetPersonSkillByIdQueryHandler : IRequestHandler<GetPersonSkillByIdQuery, PersonSkillDto>
{
    private readonly IApplicationDbContext _dbContext;

    public GetPersonSkillByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PersonSkillDto>> Handle(
        GetPersonSkillByIdQuery query,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.PersonSkills
            .FirstOrDefaultAsync(_ => _.Id == query.PersonSkillId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        return new(200, PersonSkillDto.MapFromEntity(entity));
    }
}
