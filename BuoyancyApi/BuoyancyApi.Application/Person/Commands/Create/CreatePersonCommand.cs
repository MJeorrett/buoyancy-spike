using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.Persons.Commands.Create;

public class CreatePersonCommand
{
    public string Name { get; init; } = "";
    public int RoleId { get; init; }
}

public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, int>
{
    private readonly IApplicationDbContext _dbContext;

    public CreatePersonCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<int>> Handle(
        CreatePersonCommand command,
        CancellationToken cancellationToken)
    {
        var entity = new PersonEntity
        {
            Name = command.Name,
            RoleId = command.RoleId,
        };

        _dbContext.Persons.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(201, entity.Id);
    }
}