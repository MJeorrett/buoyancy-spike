using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using BuoyancyApi.Core.Entities;

namespace BuoyancyApi.Application.Projects.Commands.Create;

public class CreateProjectCommand
{
    public string Title { get; init; } = null!;
}

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateProjectCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<int>> Handle(
        CreateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var entity = new ProjectEntity
        {
            Title = command.Title,
        };

        _dbContext.Projects.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(201, entity.Id);
    }
}
