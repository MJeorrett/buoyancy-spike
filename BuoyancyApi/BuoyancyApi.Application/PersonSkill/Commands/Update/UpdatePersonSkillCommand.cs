using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BuoyancyApi.Application.PersonSkills.Commands.Update;

public record UpdatePersonSkillCommand
{
    [JsonIgnore]
    public int PersonSkillId { get; init; }
    public int PersonId { get; init; }
    public int SkillId { get; init; }
}

public class UpdatePersonSkillCommandHandler : IRequestHandler<UpdatePersonSkillCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdatePersonSkillCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        UpdatePersonSkillCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.PersonSkills
            .FirstOrDefaultAsync(_ => _.Id == command.PersonSkillId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        entity.PersonId = command.PersonId;
        entity.SkillId = command.SkillId;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}