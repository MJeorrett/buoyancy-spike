using BuoyancyApi.Application.Projects.Commands.Delete;
using BuoyancyApi.Application.Projects.Commands.Create;
using BuoyancyApi.Application.Projects.Commands.Update;
using BuoyancyApi.Application.Projects.Dtos;
using BuoyancyApi.Application.Projects.Queries.GetById;
using BuoyancyApi.Application.Projects.Queries.List;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using BuoyancyApi.Application.Common.AppRequests.Pagination;

namespace BuoyancyApi.WebApi.Controllers;

[ApiController]
public class ProjectController : ControllerBase
{
    [HttpPost("api/projects")]
    public async Task<ActionResult<AppResponse<int>>> CreateProject(
        [FromBody] CreateProjectCommand command,
        [FromServices] CreateProjectCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/projects")]
    public async Task<ActionResult<AppResponse<PaginatedListResponse<ProjectDto>>>> ListProjects(
        [FromQuery] ListProjectsQuery query,
        [FromServices] ListProjectsQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(query, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/projects/{projectId}")]
    public async Task<ActionResult<AppResponse<ProjectDto>>> GetProjectById(
        [FromRoute] int projectId,
        [FromServices] GetProjectByIdQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { ProjectId = projectId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpPut("api/projects/{projectId}")]
    public async Task<ActionResult<AppResponse>> UpdateProject(
        [FromRoute] int projectId,
        [FromBody] UpdateProjectCommand command,
        [FromServices] UpdateProjectCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command with { ProjectId = projectId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpDelete("api/projects/{projectId}")]
    public async Task<ActionResult<AppResponse>> DeleteProject(
        [FromRoute] int projectId,
        [FromServices] DeleteProjectCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { ProjectId = projectId }, cancellationToken);

        return appResponse.ToActionResult();
    }
}