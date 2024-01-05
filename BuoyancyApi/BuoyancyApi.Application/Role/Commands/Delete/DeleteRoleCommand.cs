using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuoyancyApi.Application.Roles.Commands.Delete;

public record DeleteRoleCommand
{
    public required int RoleId { get; init; }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteRoleCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        DeleteRoleCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Roles
            .FirstOrDefaultAsync(_ => _.Id == command.RoleId, cancellationToken);

        if (entity is null)
        {
            return new(404);
        }

        _dbContext.Roles.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}
