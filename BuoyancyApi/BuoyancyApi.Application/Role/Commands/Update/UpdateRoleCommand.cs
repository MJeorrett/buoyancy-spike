using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BuoyancyApi.Application.Roles.Commands.Update;

public record UpdateRoleCommand
{
    [JsonIgnore]
    public int RoleId { get; init; }
    public string Name { get; init; } = "";
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateRoleCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        UpdateRoleCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Roles
            .FirstOrDefaultAsync(_ => _.Id == command.RoleId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        entity.Name = command.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}