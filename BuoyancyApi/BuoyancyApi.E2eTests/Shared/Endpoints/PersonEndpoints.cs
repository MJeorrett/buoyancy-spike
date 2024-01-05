using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.E2eTests.Shared.Dtos.Persons;
using BuoyancyApi.E2eTests.Shared.Endpoints.Base;
using System.Net.Http.Json;

namespace BuoyancyApi.E2eTests.Shared.Endpoints;

internal static class PersonHttpClientExtensions
{
    public static CreatePersonEndpoint CreatePerson(this HttpClient httpClient) => new(httpClient);
    public static GetPersonByIdEndpoint GetPersonById(this HttpClient httpClient) => new(httpClient);
    public static ListPersonsEndpoint ListPersons(this HttpClient httpClient) => new(httpClient);
    public static UpdatePersonEndpoint UpdatePerson(this HttpClient httpClient) => new(httpClient);
    public static DeletePersonEndpoint DeletePerson(this HttpClient httpClient) => new(httpClient);
}

internal class CreatePersonEndpoint : ApiEndpointBase<CreatePersonDto, int>
{
    internal CreatePersonEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(CreatePersonDto dto)
    {
        var path = $"api/persons";

        return await HttpClient.PostAsJsonAsync(path, dto);
    }
}

internal class GetPersonByIdEndpoint : ApiEndpointBase<int, PersonDto>
{
    internal GetPersonByIdEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int personId)
    {
        var path = $"api/persons/{personId}";

        return await HttpClient.GetAsync(path);
    }
}

internal class ListPersonsEndpoint : ApiEndpointBase<PaginatedListQuery, PaginatedListResponse<PersonDto>>
{
    internal ListPersonsEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(PaginatedListQuery dto)
    {
        var path = $"api/persons?{dto.ToQueryString()}";

        return await HttpClient.GetAsync(path);
    }
}

internal class UpdatePersonEndpoint : ApiEndpointBaseWithoutResponse<UpdatePersonDto>
{
    internal UpdatePersonEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(UpdatePersonDto dto)
    {
        var path = $"api/persons/{dto.Id}";

        return await HttpClient.PutAsJsonAsync(path, dto);
    }
}

internal class DeletePersonEndpoint : ApiEndpointBaseWithoutResponse<int>
{
    public DeletePersonEndpoint(HttpClient httpClient) : base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int personId)
    {
        return await HttpClient.DeleteAsync($"api/persons/{personId}");
    }
}
