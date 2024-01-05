using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuoyancyApi.Application.Projects.Commands.Delete;

public record DeleteProjectCommand
{
    public required int ProjectId { get; init; }
}

public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteProjectCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        DeleteProjectCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Projects
            .FirstOrDefaultAsync(_ => _.Id == command.ProjectId, cancellationToken);

        if (entity is null)
        {
            return new(404);
        }

        _dbContext.Projects.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}
