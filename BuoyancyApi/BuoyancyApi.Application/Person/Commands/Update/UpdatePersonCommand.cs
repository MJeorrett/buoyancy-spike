using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BuoyancyApi.Application.Persons.Commands.Update;

public record UpdatePersonCommand
{
    [JsonIgnore]
    public int PersonId { get; init; }
    public string Name { get; init; } = "";
    public int RoleId { get; init; }
}

public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdatePersonCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        UpdatePersonCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Persons
            .FirstOrDefaultAsync(_ => _.Id == command.PersonId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        entity.Name = command.Name;
        entity.RoleId = command.RoleId;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}