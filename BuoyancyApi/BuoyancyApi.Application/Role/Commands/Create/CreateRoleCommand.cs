using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.Roles.Commands.Create;

public class CreateRoleCommand
{
    public string Name { get; init; } = "";
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, int>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateRoleCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<int>> Handle(
        CreateRoleCommand command,
        CancellationToken cancellationToken)
    {
        var entity = new RoleEntity
        {
            Name = command.Name,
        };

        _dbContext.Roles.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(201, entity.Id);
    }
}