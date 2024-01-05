using BuoyancyApi.Application.Roles.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BuoyancyApi.Application.Roles.Queries.GetById;

public record GetRoleByIdQuery
{
    public int RoleId { get; init; }
}

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly IApplicationDbContext _dbContext;

    public GetRoleByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<RoleDto>> Handle(
        GetRoleByIdQuery query,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Roles
            .FirstOrDefaultAsync(_ => _.Id == query.RoleId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        return new(200, RoleDto.MapFromEntity(entity));
    }
}
