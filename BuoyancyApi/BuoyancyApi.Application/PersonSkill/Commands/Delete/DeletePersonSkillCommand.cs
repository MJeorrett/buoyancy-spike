using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuoyancyApi.Application.PersonSkills.Commands.Delete;

public record DeletePersonSkillCommand
{
    public required int PersonSkillId { get; init; }
}

public class DeletePersonSkillCommandHandler : IRequestHandler<DeletePersonSkillCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeletePersonSkillCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        DeletePersonSkillCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.PersonSkills
            .FirstOrDefaultAsync(_ => _.Id == command.PersonSkillId, cancellationToken);

        if (entity is null)
        {
            return new(404);
        }

        _dbContext.PersonSkills.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}
