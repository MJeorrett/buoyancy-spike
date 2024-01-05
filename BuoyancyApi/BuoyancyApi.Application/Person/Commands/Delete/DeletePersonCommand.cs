using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuoyancyApi.Application.Persons.Commands.Delete;

public record DeletePersonCommand
{
    public required int PersonId { get; init; }
}

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeletePersonCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        DeletePersonCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Persons
            .FirstOrDefaultAsync(_ => _.Id == command.PersonId, cancellationToken);

        if (entity is null)
        {
            return new(404);
        }

        _dbContext.Persons.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}
