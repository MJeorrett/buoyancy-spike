using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuoyancyApi.Application.RequiredTimes.Commands.Delete;

public record DeleteRequiredTimeCommand
{
    public required int RequiredTimeId { get; init; }
}

public class DeleteRequiredTimeCommandHandler : IRequestHandler<DeleteRequiredTimeCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteRequiredTimeCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        DeleteRequiredTimeCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.RequiredTimes
            .FirstOrDefaultAsync(_ => _.Id == command.RequiredTimeId, cancellationToken);

        if (entity is null)
        {
            return new(404);
        }

        _dbContext.RequiredTimes.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}
