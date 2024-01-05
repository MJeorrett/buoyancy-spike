using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.Skills.Commands.Create;

public class CreateSkillCommand
{
    public string Name { get; init; } = "";
}

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, int>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateSkillCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<int>> Handle(
        CreateSkillCommand command,
        CancellationToken cancellationToken)
    {
        var entity = new SkillEntity
        {
            Name = command.Name,
        };

        _dbContext.Skills.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(201, entity.Id);
    }
}