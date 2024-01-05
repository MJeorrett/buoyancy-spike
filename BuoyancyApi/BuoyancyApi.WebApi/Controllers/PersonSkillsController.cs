using BuoyancyApi.Application.PersonSkills.Commands.Create;
using BuoyancyApi.Application.PersonSkills.Commands.Delete;
using BuoyancyApi.Application.PersonSkills.Commands.Update;
using BuoyancyApi.Application.PersonSkills.Dtos;
using BuoyancyApi.Application.PersonSkills.Queries.GetById;
using BuoyancyApi.Application.PersonSkills.Queries.List;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BuoyancyApi.WebApi.Controllers;

[ApiController]
public class PersonSkillController : ControllerBase
{
    [HttpPost("api/personskills")]
    public async Task<ActionResult<AppResponse<int>>> CreatePersonSkill(
        [FromBody] CreatePersonSkillCommand command,
        [FromServices] CreatePersonSkillCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/personskills")]
    public async Task<ActionResult<AppResponse<PaginatedListResponse<PersonSkillDto>>>> ListPersonSkills(
        [FromQuery] ListPersonSkillsQuery query,
        [FromServices] ListPersonSkillsQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(query, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/personskills/{personSkillId}")]
    public async Task<ActionResult<AppResponse<PersonSkillDto>>> GetPersonSkillById(
        [FromRoute] int personSkillId,
        [FromServices] GetPersonSkillByIdQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { PersonSkillId = personSkillId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpPut("api/personskills/{personSkillId}")]
    public async Task<ActionResult<AppResponse>> UpdatePersonSkill(
        [FromRoute] int personSkillId,
        [FromBody] UpdatePersonSkillCommand command,
        [FromServices] UpdatePersonSkillCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command with { PersonSkillId = personSkillId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    

    [HttpDelete("api/personskills/{personSkillId}")]
    public async Task<ActionResult<AppResponse>> DeletePersonSkill(
        [FromRoute] int personSkillId,
        [FromServices] DeletePersonSkillCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { PersonSkillId = personSkillId }, cancellationToken);

        return appResponse.ToActionResult();
    }
}