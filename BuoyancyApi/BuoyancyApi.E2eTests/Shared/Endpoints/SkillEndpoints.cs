using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.E2eTests.Shared.Dtos.Skills;
using BuoyancyApi.E2eTests.Shared.Endpoints.Base;
using System.Net.Http.Json;

namespace BuoyancyApi.E2eTests.Shared.Endpoints;

internal static class SkillHttpClientExtensions
{
    public static CreateSkillEndpoint CreateSkill(this HttpClient httpClient) => new(httpClient);
    public static GetSkillByIdEndpoint GetSkillById(this HttpClient httpClient) => new(httpClient);
    public static ListSkillsEndpoint ListSkills(this HttpClient httpClient) => new(httpClient);
    public static UpdateSkillEndpoint UpdateSkill(this HttpClient httpClient) => new(httpClient);
    public static DeleteSkillEndpoint DeleteSkill(this HttpClient httpClient) => new(httpClient);
}

internal class CreateSkillEndpoint : ApiEndpointBase<CreateSkillDto, int>
{
    internal CreateSkillEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(CreateSkillDto dto)
    {
        var path = $"api/skills";

        return await HttpClient.PostAsJsonAsync(path, dto);
    }
}

internal class GetSkillByIdEndpoint : ApiEndpointBase<int, SkillDto>
{
    internal GetSkillByIdEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int skillId)
    {
        var path = $"api/skills/{skillId}";

        return await HttpClient.GetAsync(path);
    }
}

internal class ListSkillsEndpoint : ApiEndpointBase<PaginatedListQuery, PaginatedListResponse<SkillDto>>
{
    internal ListSkillsEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(PaginatedListQuery dto)
    {
        var path = $"api/skills?{dto.ToQueryString()}";

        return await HttpClient.GetAsync(path);
    }
}

internal class UpdateSkillEndpoint : ApiEndpointBaseWithoutResponse<UpdateSkillDto>
{
    internal UpdateSkillEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(UpdateSkillDto dto)
    {
        var path = $"api/skills/{dto.Id}";

        return await HttpClient.PutAsJsonAsync(path, dto);
    }
}

internal class DeleteSkillEndpoint : ApiEndpointBaseWithoutResponse<int>
{
    public DeleteSkillEndpoint(HttpClient httpClient) : base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int skillId)
    {
        return await HttpClient.DeleteAsync($"api/skills/{skillId}");
    }
}
