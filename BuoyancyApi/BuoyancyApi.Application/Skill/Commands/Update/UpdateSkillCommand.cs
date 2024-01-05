using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BuoyancyApi.Application.Skills.Commands.Update;

public record UpdateSkillCommand
{
    [JsonIgnore]
    public int SkillId { get; init; }
    public string Name { get; init; } = "";
}

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateSkillCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        UpdateSkillCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Skills
            .FirstOrDefaultAsync(_ => _.Id == command.SkillId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        entity.Name = command.Name;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}