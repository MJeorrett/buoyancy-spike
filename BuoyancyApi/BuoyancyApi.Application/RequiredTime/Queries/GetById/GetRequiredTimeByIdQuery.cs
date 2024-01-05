using BuoyancyApi.Application.RequiredTimes.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BuoyancyApi.Application.RequiredTimes.Queries.GetById;

public record GetRequiredTimeByIdQuery
{
    public int RequiredTimeId { get; init; }
}

public class GetRequiredTimeByIdQueryHandler : IRequestHandler<GetRequiredTimeByIdQuery, RequiredTimeDto>
{
    private readonly IApplicationDbContext _dbContext;

    public GetRequiredTimeByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<RequiredTimeDto>> Handle(
        GetRequiredTimeByIdQuery query,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.RequiredTimes
            .FirstOrDefaultAsync(_ => _.Id == query.RequiredTimeId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        return new(200, RequiredTimeDto.MapFromEntity(entity));
    }
}
