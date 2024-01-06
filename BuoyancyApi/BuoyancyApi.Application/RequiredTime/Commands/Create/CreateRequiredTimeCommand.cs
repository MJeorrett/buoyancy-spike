using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.RequiredTimes.Commands.Create;

public class CreateRequiredTimeCommand
{
    public int ProjectId { get; init; }
    public int RoleId { get; init; }
    public int SkillId { get; init; }
    public DateOnly WeekStartingMonday { get; init; }
    public decimal Hours { get; init; }
}

public class CreateRequiredTimeCommandHandler : IRequestHandler<CreateRequiredTimeCommand, int>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateRequiredTimeCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<int>> Handle(
        CreateRequiredTimeCommand command,
        CancellationToken cancellationToken)
    {
        var entity = new RequiredTimeEntity
        {
            ProjectId = command.ProjectId,
            RoleId = command.RoleId,
            SkillId = command.SkillId,
            WeekStartingMonday = command.WeekStartingMonday,
            Hours = command.Hours,
        };

        _dbContext.RequiredTimes.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(201, entity.Id);
    }
}