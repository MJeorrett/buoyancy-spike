using BuoyancyApi.Application.Roles.Commands.Create;
using BuoyancyApi.Application.Roles.Commands.Delete;
using BuoyancyApi.Application.Roles.Commands.Update;
using BuoyancyApi.Application.Roles.Dtos;
using BuoyancyApi.Application.Roles.Queries.GetById;
using BuoyancyApi.Application.Roles.Queries.List;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BuoyancyApi.WebApi.Controllers;

[ApiController]
public class RoleController : ControllerBase
{
    [HttpPost("api/roles")]
    public async Task<ActionResult<AppResponse<int>>> CreateRole(
        [FromBody] CreateRoleCommand command,
        [FromServices] CreateRoleCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/roles")]
    public async Task<ActionResult<AppResponse<PaginatedListResponse<RoleDto>>>> ListRoles(
        [FromQuery] ListRolesQuery query,
        [FromServices] ListRolesQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(query, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/roles/{roleId}")]
    public async Task<ActionResult<AppResponse<RoleDto>>> GetRoleById(
        [FromRoute] int roleId,
        [FromServices] GetRoleByIdQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { RoleId = roleId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpPut("api/roles/{roleId}")]
    public async Task<ActionResult<AppResponse>> UpdateRole(
        [FromRoute] int roleId,
        [FromBody] UpdateRoleCommand command,
        [FromServices] UpdateRoleCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command with { RoleId = roleId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    

    [HttpDelete("api/roles/{roleId}")]
    public async Task<ActionResult<AppResponse>> DeleteRole(
        [FromRoute] int roleId,
        [FromServices] DeleteRoleCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { RoleId = roleId }, cancellationToken);

        return appResponse.ToActionResult();
    }
}