using BuoyancyApi.Application.NonProjectTimeTypes.Dtos;
using BuoyancyApi.Application.NonProjectTimeTypes.Queries.GetById;
using BuoyancyApi.Application.NonProjectTimeTypes.Queries.List;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BuoyancyApi.WebApi.Controllers;

[ApiController]
public class NonProjectTimeTypeController : ControllerBase
{
    [HttpGet("api/nonprojecttimetypes")]
    public async Task<ActionResult<AppResponse<PaginatedListResponse<NonProjectTimeTypeDto>>>> ListNonProjectTimeTypes(
        [FromQuery] ListNonProjectTimeTypesQuery query,
        [FromServices] ListNonProjectTimeTypesQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(query, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/nonprojecttimetypes/{nonProjectTimeTypeId}")]
    public async Task<ActionResult<AppResponse<NonProjectTimeTypeDto>>> GetNonProjectTimeTypeById(
        [FromRoute] int nonProjectTimeTypeId,
        [FromServices] GetNonProjectTimeTypeByIdQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { NonProjectTimeTypeId = nonProjectTimeTypeId }, cancellationToken);

        return appResponse.ToActionResult();
    }
}