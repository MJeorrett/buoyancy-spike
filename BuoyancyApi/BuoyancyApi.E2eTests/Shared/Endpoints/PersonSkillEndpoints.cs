using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.E2eTests.Shared.Dtos.PersonSkills;
using BuoyancyApi.E2eTests.Shared.Endpoints.Base;
using System.Net.Http.Json;

namespace BuoyancyApi.E2eTests.Shared.Endpoints;

internal static class PersonSkillHttpClientExtensions
{
    public static CreatePersonSkillEndpoint CreatePersonSkill(this HttpClient httpClient) => new(httpClient);
    public static GetPersonSkillByIdEndpoint GetPersonSkillById(this HttpClient httpClient) => new(httpClient);
    public static ListPersonSkillsEndpoint ListPersonSkills(this HttpClient httpClient) => new(httpClient);
    public static UpdatePersonSkillEndpoint UpdatePersonSkill(this HttpClient httpClient) => new(httpClient);
    public static DeletePersonSkillEndpoint DeletePersonSkill(this HttpClient httpClient) => new(httpClient);
}

internal class CreatePersonSkillEndpoint : ApiEndpointBase<CreatePersonSkillDto, int>
{
    internal CreatePersonSkillEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(CreatePersonSkillDto dto)
    {
        var path = $"api/personskills";

        return await HttpClient.PostAsJsonAsync(path, dto);
    }
}

internal class GetPersonSkillByIdEndpoint : ApiEndpointBase<int, PersonSkillDto>
{
    internal GetPersonSkillByIdEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int personSkillId)
    {
        var path = $"api/personskills/{personSkillId}";

        return await HttpClient.GetAsync(path);
    }
}

internal class ListPersonSkillsEndpoint : ApiEndpointBase<PaginatedListQuery, PaginatedListResponse<PersonSkillDto>>
{
    internal ListPersonSkillsEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(PaginatedListQuery dto)
    {
        var path = $"api/personskills?{dto.ToQueryString()}";

        return await HttpClient.GetAsync(path);
    }
}

internal class UpdatePersonSkillEndpoint : ApiEndpointBaseWithoutResponse<UpdatePersonSkillDto>
{
    internal UpdatePersonSkillEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(UpdatePersonSkillDto dto)
    {
        var path = $"api/personskills/{dto.Id}";

        return await HttpClient.PutAsJsonAsync(path, dto);
    }
}

internal class DeletePersonSkillEndpoint : ApiEndpointBaseWithoutResponse<int>
{
    public DeletePersonSkillEndpoint(HttpClient httpClient) : base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int personSkillId)
    {
        return await HttpClient.DeleteAsync($"api/personskills/{personSkillId}");
    }
}
