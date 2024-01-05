using BuoyancyApi.Application.Common.AppRequests.Pagination;
using BuoyancyApi.E2eTests.Shared.Dtos.Projects;
using BuoyancyApi.E2eTests.Shared.Endpoints.Base;
using System.Net.Http.Json;

namespace BuoyancyApi.E2eTests.Shared.Endpoints;

internal static class ProjectHttpClientExtensions
{
    public static CreateProjectEndpoint CreateProject(this HttpClient httpClient) => new(httpClient);
    public static GetProjectByIdEndpoint GetProjectById(this HttpClient httpClient) => new(httpClient);
    public static ListProjectsEndpoint ListProjects(this HttpClient httpClient) => new(httpClient);
    public static UpdateProjectEndpoint UpdateProject(this HttpClient httpClient) => new(httpClient);
    public static DeleteProjectEndpoint DeleteProject(this HttpClient httpClient) => new(httpClient);
}

internal class CreateProjectEndpoint : ApiEndpointBase<CreateProjectDto, int>
{
    internal CreateProjectEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(CreateProjectDto dto)
    {
        var path = "api/projects";

        return await HttpClient.PostAsJsonAsync(path, dto);
    }
}

internal class GetProjectByIdEndpoint : ApiEndpointBase<int, ProjectDto>
{
    internal GetProjectByIdEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int projectId)
    {
        var path = $"api/projects/{projectId}";

        return await HttpClient.GetAsync(path);
    }
}

internal class ListProjectsEndpoint : ApiEndpointBase<PaginatedListQuery, PaginatedListResponse<ProjectDto>>
{
    public ListProjectsEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(PaginatedListQuery dto)
    {
        var path = $"api/projects?{dto.ToQueryString()}";

        return await HttpClient.GetAsync(path);
    }
}

internal class UpdateProjectEndpoint : ApiEndpointBaseWithoutResponse<UpdateProjectDto>
{
    internal UpdateProjectEndpoint(HttpClient httpClient) :
        base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(UpdateProjectDto dto)
    {
        var path = $"api/projects/{dto.Id}";

        return await HttpClient.PutAsJsonAsync(path, dto);
    }
}

internal class DeleteProjectEndpoint : ApiEndpointBaseWithoutResponse<int>
{
    public DeleteProjectEndpoint(HttpClient httpClient) : base(httpClient)
    {
    }

    public override async Task<HttpResponseMessage> Call(int projectId)
    {
        return await HttpClient.DeleteAsync($"api/projects/{projectId}");
    }
}