using BuoyancyApi.Application.Skills.Commands.Create;
using BuoyancyApi.Application.Skills.Commands.Delete;
using BuoyancyApi.Application.Skills.Commands.Update;
using BuoyancyApi.Application.Skills.Dtos;
using BuoyancyApi.Application.Skills.Queries.GetById;
using BuoyancyApi.Application.Skills.Queries.List;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BuoyancyApi.WebApi.Controllers;

[ApiController]
public class SkillController : ControllerBase
{
    [HttpPost("api/skills")]
    public async Task<ActionResult<AppResponse<int>>> CreateSkill(
        [FromBody] CreateSkillCommand command,
        [FromServices] CreateSkillCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/skills")]
    public async Task<ActionResult<AppResponse<PaginatedListResponse<SkillDto>>>> ListSkills(
        [FromQuery] ListSkillsQuery query,
        [FromServices] ListSkillsQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(query, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/skills/{skillId}")]
    public async Task<ActionResult<AppResponse<SkillDto>>> GetSkillById(
        [FromRoute] int skillId,
        [FromServices] GetSkillByIdQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { SkillId = skillId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpPut("api/skills/{skillId}")]
    public async Task<ActionResult<AppResponse>> UpdateSkill(
        [FromRoute] int skillId,
        [FromBody] UpdateSkillCommand command,
        [FromServices] UpdateSkillCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command with { SkillId = skillId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    

    [HttpDelete("api/skills/{skillId}")]
    public async Task<ActionResult<AppResponse>> DeleteSkill(
        [FromRoute] int skillId,
        [FromServices] DeleteSkillCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { SkillId = skillId }, cancellationToken);

        return appResponse.ToActionResult();
    }
}