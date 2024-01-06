using BuoyancyApi.Application.PlannedTimes.Commands.Create;
using BuoyancyApi.Application.PlannedTimes.Commands.Delete;
using BuoyancyApi.Application.PlannedTimes.Commands.Update;
using BuoyancyApi.Application.PlannedTimes.Dtos;
using BuoyancyApi.Application.PlannedTimes.Queries.GetById;
using BuoyancyApi.Application.PlannedTimes.Queries.List;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;
using BuoyancyApi.Application.PlannedTime.Commands.Upsert;

namespace BuoyancyApi.WebApi.Controllers;

[ApiController]
public class PlannedTimeController : ControllerBase
{
    [HttpPost("api/plannedtimes")]
    public async Task<ActionResult<AppResponse<int>>> CreatePlannedTime(
        [FromBody] CreatePlannedTimeCommand command,
        [FromServices] CreatePlannedTimeCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpPut("api/projects/{projectId}/plannedtimes/{weekStartingMonday}")]
    public async Task<ActionResult<AppResponse<int>>> UpsertPlannedTime(
        [FromRoute] int projectId,
        [FromRoute] DateOnly weekStartingMonday,
        [FromBody] UpsertPlannedTimeCommand command,
        [FromServices] UpsertPlannedTimeCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command with { ProjectId = projectId, WeekStartingMonday = weekStartingMonday }, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/plannedtimes")]
    public async Task<ActionResult<AppResponse<PaginatedListResponse<PlannedTimeDto>>>> ListPlannedTimes(
        [FromQuery] ListPlannedTimesQuery query,
        [FromServices] ListPlannedTimesQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(query, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/plannedtimes/{plannedTimeId}")]
    public async Task<ActionResult<AppResponse<PlannedTimeDto>>> GetPlannedTimeById(
        [FromRoute] int plannedTimeId,
        [FromServices] GetPlannedTimeByIdQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { PlannedTimeId = plannedTimeId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpPut("api/plannedtimes/{plannedTimeId}")]
    public async Task<ActionResult<AppResponse>> UpdatePlannedTime(
        [FromRoute] int plannedTimeId,
        [FromBody] UpdatePlannedTimeCommand command,
        [FromServices] UpdatePlannedTimeCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command with { PlannedTimeId = plannedTimeId }, cancellationToken);

        return appResponse.ToActionResult();
    }



    [HttpDelete("api/plannedtimes/{plannedTimeId}")]
    public async Task<ActionResult<AppResponse>> DeletePlannedTime(
        [FromRoute] int plannedTimeId,
        [FromServices] DeletePlannedTimeCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { PlannedTimeId = plannedTimeId }, cancellationToken);

        return appResponse.ToActionResult();
    }
}