using BuoyancyApi.Application.Persons.Commands.Create;
using BuoyancyApi.Application.Persons.Commands.Delete;
using BuoyancyApi.Application.Persons.Commands.Update;
using BuoyancyApi.Application.Persons.Dtos;
using BuoyancyApi.Application.Persons.Queries.GetById;
using BuoyancyApi.Application.Persons.Queries.List;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BuoyancyApi.WebApi.Controllers;

[ApiController]
public class PersonController : ControllerBase
{
    [HttpPost("api/persons")]
    public async Task<ActionResult<AppResponse<int>>> CreatePerson(
        [FromBody] CreatePersonCommand command,
        [FromServices] CreatePersonCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/persons")]
    public async Task<ActionResult<AppResponse<PaginatedListResponse<PersonDto>>>> ListPersons(
        [FromQuery] ListPersonsQuery query,
        [FromServices] ListPersonsQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(query, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpGet("api/persons/{personId}")]
    public async Task<ActionResult<AppResponse<PersonDto>>> GetPersonById(
        [FromRoute] int personId,
        [FromServices] GetPersonByIdQueryHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { PersonId = personId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    [HttpPut("api/persons/{personId}")]
    public async Task<ActionResult<AppResponse>> UpdatePerson(
        [FromRoute] int personId,
        [FromBody] UpdatePersonCommand command,
        [FromServices] UpdatePersonCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(command with { PersonId = personId }, cancellationToken);

        return appResponse.ToActionResult();
    }

    

    [HttpDelete("api/persons/{personId}")]
    public async Task<ActionResult<AppResponse>> DeletePerson(
        [FromRoute] int personId,
        [FromServices] DeletePersonCommandHandler handler,
        CancellationToken cancellationToken)
    {
        var appResponse = await handler.Handle(new() { PersonId = personId }, cancellationToken);

        return appResponse.ToActionResult();
    }
}