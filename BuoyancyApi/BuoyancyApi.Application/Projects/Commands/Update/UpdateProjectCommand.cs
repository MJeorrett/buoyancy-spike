using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BuoyancyApi.Application.Projects.Commands.Update;

public record UpdateProjectCommand
{
    [JsonIgnore]
    public int ProjectId { get; init; }

    public string Title { get; init; } = "";
}

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateProjectCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse> Handle(
        UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Projects
            .FirstOrDefaultAsync(_ => _.Id == command.ProjectId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        entity.Title = command.Title;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(200);
    }
}
