using BuoyancyApi.Application.PlannedTimes.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BuoyancyApi.Application.PlannedTimes.Queries.GetById;

public record GetPlannedTimeByIdQuery
{
    public int PlannedTimeId { get; init; }
}

public class GetPlannedTimeByIdQueryHandler : IRequestHandler<GetPlannedTimeByIdQuery, PlannedTimeDto>
{
    private readonly IApplicationDbContext _dbContext;

    public GetPlannedTimeByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PlannedTimeDto>> Handle(
        GetPlannedTimeByIdQuery query,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.PlannedTimes
            .FirstOrDefaultAsync(_ => _.Id == query.PlannedTimeId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        return new(200, PlannedTimeDto.MapFromEntity(entity));
    }
}
