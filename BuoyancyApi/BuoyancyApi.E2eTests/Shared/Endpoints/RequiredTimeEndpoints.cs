using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.E2eTests.Shared.Dtos.RequiredTimes;
using BuoyancyApi.E2eTests.Shared.Endpoints.Base;
using System.Net.Http.Json;

namespace BuoyancyApi.E2eTests.Shared.Endpoints;

internal static class RequiredTimeHttpClientExtensions
{
    public static CreateRequiredTimeEndpoint CreateRequiredTime(this HttpClient httpClient) => new(httpClient);
    public static GetRequiredTimeByIdEndpoint GetRequiredTimeById(this HttpClient httpClient) => new(httpClient);
    public static ListRequiredTimesEndpoint ListRequiredTimes(this HttpClient httpClient) => new(httpClient);
    public static UpdateRequiredTimeEndpoint UpdateRequiredTime(this HttpClient httpClient) => new(httpClient);
    public static DeleteRequiredTimeEndpoint DeleteRequiredTime(this HttpClient httpClient) => new(httpClient);
}

internal class CreateRequiredTimeEndpoint : ApiEndpointBase<CreateRequiredTimeDto, int>
{
    internal CreateRequiredTimeEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(CreateRequiredTimeDto dto)
    {
        var path = $"api/requiredtimes";

        return await HttpClient.PostAsJsonAsync(path, dto);
    }
}

internal class GetRequiredTimeByIdEndpoint : ApiEndpointBase<int, RequiredTimeDto>
{
    internal GetRequiredTimeByIdEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int requiredTimeId)
    {
        var path = $"api/requiredtimes/{requiredTimeId}";

        return await HttpClient.GetAsync(path);
    }
}

internal class ListRequiredTimesEndpoint : ApiEndpointBase<PaginatedListQuery, PaginatedListResponse<RequiredTimeDto>>
{
    internal ListRequiredTimesEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(PaginatedListQuery dto)
    {
        var path = $"api/requiredtimes?{dto.ToQueryString()}";

        return await HttpClient.GetAsync(path);
    }
}

internal class UpdateRequiredTimeEndpoint : ApiEndpointBaseWithoutResponse<UpdateRequiredTimeDto>
{
    internal UpdateRequiredTimeEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(UpdateRequiredTimeDto dto)
    {
        var path = $"api/requiredtimes/{dto.Id}";

        return await HttpClient.PutAsJsonAsync(path, dto);
    }
}

internal class DeleteRequiredTimeEndpoint : ApiEndpointBaseWithoutResponse<int>
{
    public DeleteRequiredTimeEndpoint(HttpClient httpClient) : base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int requiredTimeId)
    {
        return await HttpClient.DeleteAsync($"api/requiredtimes/{requiredTimeId}");
    }
}
