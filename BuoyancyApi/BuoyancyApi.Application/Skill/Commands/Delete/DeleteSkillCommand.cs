using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuoyancyApi.Application.Skills.Commands.Delete;

public record DeleteSkillCommand
{
    public required int SkillId { get; init; }
}

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteSkillCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        DeleteSkillCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Skills
            .FirstOrDefaultAsync(_ => _.Id == command.SkillId, cancellationToken);

        if (entity is null)
        {
            return new(404);
        }

        _dbContext.Skills.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}
