using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuoyancyApi.Application.PlannedTimes.Commands.Delete;

public record DeletePlannedTimeCommand
{
    public required int PlannedTimeId { get; init; }
}

public class DeletePlannedTimeCommandHandler : IRequestHandler<DeletePlannedTimeCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeletePlannedTimeCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        DeletePlannedTimeCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.PlannedTimes
            .FirstOrDefaultAsync(_ => _.Id == command.PlannedTimeId, cancellationToken);

        if (entity is null)
        {
            return new(404);
        }

        _dbContext.PlannedTimes.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}
