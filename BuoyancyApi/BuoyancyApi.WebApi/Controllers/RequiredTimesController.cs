using BuoyancyApi.Application.RequiredTimes.Commands.Create;
using BuoyancyApi.Application.RequiredTimes.Commands.Delete;
using BuoyancyApi.Application.RequiredTimes.Commands.Update;
using BuoyancyApi.Application.RequiredTimes.Dtos;
using BuoyancyApi.Application.RequiredTimes.Queries.GetById;
using BuoyancyApi.Application.RequiredTimes.Queries.List;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BuoyancyApi.WebApi.Controllers;

[ApiController]
public class RequiredTimeController : ControllerBase
{
    [HttpPost("api/requiredtimes")]
    public async Task<ActionResult<AppResponse<int>>> CreateRequiredTime(
        [FromBody] CreateRequiredTimeCommand command,
        [FromServices] CreateRequiredTimeCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/requiredtimes")]
    public async Task<ActionResult<AppResponse<PaginatedListResponse<RequiredTimeDto>>>> ListRequiredTimes(
        [FromQuery] ListRequiredTimesQuery query,
        [FromServices] ListRequiredTimesQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(query, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/requiredtimes/{requiredTimeId}")]
    public async Task<ActionResult<AppResponse<RequiredTimeDto>>> GetRequiredTimeById(
        [FromRoute] int requiredTimeId,
        [FromServices] GetRequiredTimeByIdQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { RequiredTimeId = requiredTimeId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpPut("api/requiredtimes/{requiredTimeId}")]
    public async Task<ActionResult<AppResponse>> UpdateRequiredTime(
        [FromRoute] int requiredTimeId,
        [FromBody] UpdateRequiredTimeCommand command,
        [FromServices] UpdateRequiredTimeCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command with { RequiredTimeId = requiredTimeId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    

    [HttpDelete("api/requiredtimes/{requiredTimeId}")]
    public async Task<ActionResult<AppResponse>> DeleteRequiredTime(
        [FromRoute] int requiredTimeId,
        [FromServices] DeleteRequiredTimeCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { RequiredTimeId = requiredTimeId }, cancellationToken);

        return appResponse.ToActionResult();
    }
}