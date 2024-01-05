using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.E2eTests.Shared.Dtos.PlannedTimes;
using BuoyancyApi.E2eTests.Shared.Endpoints.Base;
using System.Net.Http.Json;

namespace BuoyancyApi.E2eTests.Shared.Endpoints;

internal static class PlannedTimeHttpClientExtensions
{
    public static CreatePlannedTimeEndpoint CreatePlannedTime(this HttpClient httpClient) => new(httpClient);
    public static GetPlannedTimeByIdEndpoint GetPlannedTimeById(this HttpClient httpClient) => new(httpClient);
    public static ListPlannedTimesEndpoint ListPlannedTimes(this HttpClient httpClient) => new(httpClient);
    public static UpdatePlannedTimeEndpoint UpdatePlannedTime(this HttpClient httpClient) => new(httpClient);
    public static DeletePlannedTimeEndpoint DeletePlannedTime(this HttpClient httpClient) => new(httpClient);
}

internal class CreatePlannedTimeEndpoint : ApiEndpointBase<CreatePlannedTimeDto, int>
{
    internal CreatePlannedTimeEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(CreatePlannedTimeDto dto)
    {
        var path = $"api/plannedtimes";

        return await HttpClient.PostAsJsonAsync(path, dto);
    }
}

internal class GetPlannedTimeByIdEndpoint : ApiEndpointBase<int, PlannedTimeDto>
{
    internal GetPlannedTimeByIdEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int plannedTimeId)
    {
        var path = $"api/plannedtimes/{plannedTimeId}";

        return await HttpClient.GetAsync(path);
    }
}

internal class ListPlannedTimesEndpoint : ApiEndpointBase<PaginatedListQuery, PaginatedListResponse<PlannedTimeDto>>
{
    internal ListPlannedTimesEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(PaginatedListQuery dto)
    {
        var path = $"api/plannedtimes?{dto.ToQueryString()}";

        return await HttpClient.GetAsync(path);
    }
}

internal class UpdatePlannedTimeEndpoint : ApiEndpointBaseWithoutResponse<UpdatePlannedTimeDto>
{
    internal UpdatePlannedTimeEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(UpdatePlannedTimeDto dto)
    {
        var path = $"api/plannedtimes/{dto.Id}";

        return await HttpClient.PutAsJsonAsync(path, dto);
    }
}

internal class DeletePlannedTimeEndpoint : ApiEndpointBaseWithoutResponse<int>
{
    public DeletePlannedTimeEndpoint(HttpClient httpClient) : base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int plannedTimeId)
    {
        return await HttpClient.DeleteAsync($"api/plannedtimes/{plannedTimeId}");
    }
}
