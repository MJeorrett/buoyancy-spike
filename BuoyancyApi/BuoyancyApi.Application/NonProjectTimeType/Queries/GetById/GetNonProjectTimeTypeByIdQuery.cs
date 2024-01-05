using BuoyancyApi.Application.NonProjectTimeTypes.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BuoyancyApi.Application.NonProjectTimeTypes.Queries.GetById;

public record GetNonProjectTimeTypeByIdQuery
{
    public int NonProjectTimeTypeId { get; init; }
}

public class GetNonProjectTimeTypeByIdQueryHandler : IRequestHandler<GetNonProjectTimeTypeByIdQuery, NonProjectTimeTypeDto>
{
    private readonly IApplicationDbContext _dbContext;

    public GetNonProjectTimeTypeByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<NonProjectTimeTypeDto>> Handle(
        GetNonProjectTimeTypeByIdQuery query,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.NonProjectTimeTypes
            .FirstOrDefaultAsync(_ => _.Id == query.NonProjectTimeTypeId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        return new(200, NonProjectTimeTypeDto.MapFromEntity(entity));
    }
}
