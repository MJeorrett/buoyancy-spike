using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.PersonSkills.Commands.Create;

public class CreatePersonSkillCommand
{
    public int PersonId { get; init; }
    public int SkillId { get; init; }
}

public class CreatePersonSkillCommandHandler : IRequestHandler<CreatePersonSkillCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public CreatePersonSkillCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        CreatePersonSkillCommand command,
        CancellationToken cancellationToken)
    {
        var entity = new PersonSkillEntity
        {
            PersonId = command.PersonId,
            SkillId = command.SkillId,
        };

        _dbContext.PersonSkills.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(201);
    }
}