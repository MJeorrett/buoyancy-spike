using BuoyancyApi.Application.Persons.Dtos;
using BuoyancyApi.Application.Common.AppRequests;
using BuoyancyApi.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BuoyancyApi.Application.Persons.Queries.GetById;

public record GetPersonByIdQuery
{
    public int PersonId { get; init; }
}

public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, PersonDto>
{
    private readonly IApplicationDbContext _dbContext;

    public GetPersonByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AppResponse<PersonDto>> Handle(
        GetPersonByIdQuery query,
        CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Persons
            .Include(_ => _.Role)
            .Include(_ => _.PlannedTime)
            .FirstOrDefaultAsync(_ => _.Id == query.PersonId, cancellationToken);

        if (entity == null)
        {
            return new(404);
        }

        return new(200, PersonDto.MapFromEntity(entity));
    }
}
