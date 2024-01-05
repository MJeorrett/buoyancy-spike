using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.E2eTests.Shared.Dtos.Roles;
using BuoyancyApi.E2eTests.Shared.Endpoints.Base;
using System.Net.Http.Json;

namespace BuoyancyApi.E2eTests.Shared.Endpoints;

internal static class RoleHttpClientExtensions
{
    public static CreateRoleEndpoint CreateRole(this HttpClient httpClient) => new(httpClient);
    public static GetRoleByIdEndpoint GetRoleById(this HttpClient httpClient) => new(httpClient);
    public static ListRolesEndpoint ListRoles(this HttpClient httpClient) => new(httpClient);
    public static UpdateRoleEndpoint UpdateRole(this HttpClient httpClient) => new(httpClient);
    public static DeleteRoleEndpoint DeleteRole(this HttpClient httpClient) => new(httpClient);
}

internal class CreateRoleEndpoint : ApiEndpointBase<CreateRoleDto, int>
{
    internal CreateRoleEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(CreateRoleDto dto)
    {
        var path = $"api/roles";

        return await HttpClient.PostAsJsonAsync(path, dto);
    }
}

internal class GetRoleByIdEndpoint : ApiEndpointBase<int, RoleDto>
{
    internal GetRoleByIdEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int roleId)
    {
        var path = $"api/roles/{roleId}";

        return await HttpClient.GetAsync(path);
    }
}

internal class ListRolesEndpoint : ApiEndpointBase<PaginatedListQuery, PaginatedListResponse<RoleDto>>
{
    internal ListRolesEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(PaginatedListQuery dto)
    {
        var path = $"api/roles?{dto.ToQueryString()}";

        return await HttpClient.GetAsync(path);
    }
}

internal class UpdateRoleEndpoint : ApiEndpointBaseWithoutResponse<UpdateRoleDto>
{
    internal UpdateRoleEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(UpdateRoleDto dto)
    {
        var path = $"api/roles/{dto.Id}";

        return await HttpClient.PutAsJsonAsync(path, dto);
    }
}

internal class DeleteRoleEndpoint : ApiEndpointBaseWithoutResponse<int>
{
    public DeleteRoleEndpoint(HttpClient httpClient) : base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int roleId)
    {
        return await HttpClient.DeleteAsync($"api/roles/{roleId}");
    }
}
