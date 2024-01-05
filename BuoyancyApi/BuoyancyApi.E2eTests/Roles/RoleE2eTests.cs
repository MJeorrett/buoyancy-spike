using BuoyancyApi.E2eTests.Shared.Dtos.Roles;
using BuoyancyApi.E2eTests.Shared.Endpoints;
using BuoyancyApi.E2eTests.Shared.Extensions;
using BuoyancyApi.E2eTests.Shared.WebApplicationFactory;
using FluentAssertions;
using Xunit.Abstractions;

namespace BuoyancyApi.E2eTests.Roles;

[Collection(CustomWebApplicationCollection.Name)]
public class RoleE2eTests : TestBase
{
    public RoleE2eTests(
        CustomWebApplicationFixture webApplicationFixture,
        ITestOutputHelper testOutputHelper) :
        base(webApplicationFixture, testOutputHelper)
    {
    }

    [Fact]
    public async Task ShouldReturnCreatedRoleById()
    {
        var createResponse = await HttpClient.CreateRole().Call(new() {
        Name = "Test Name 1",
        });

        await createResponse.Should().HaveStatusCode(201);

        var createdRoleId = await createResponse.ReadResponseContentAs<int>();

        var getRoleByIdResponse = await HttpClient.GetRoleById().Call(createdRoleId);

        await getRoleByIdResponse.Should().HaveStatusCode(200);

        var returnedRole = await getRoleByIdResponse.ReadResponseContentAs<RoleDto>();

        returnedRole.Name.Should().Be("Test Name 1");
    }

    [Fact]
    public async Task ShouldListCreatedRoles()
    {
        var role1Id = await HttpClient.CreateRole().CallAndParseResponse(new() {
        Name = "Test Name 1",
        });
        var role2Id = await HttpClient.CreateRole().CallAndParseResponse(new() {
        Name = "Test Name 2",
        });

        var listRolesResult = await HttpClient.ListRoles().CallAndParseResponse(new()
        {
            PageIndex = 1,
            PageSize = 5,
        });

        listRolesResult.Items.Should().HaveCount(2);
        listRolesResult.Items[0].Should().BeEquivalentTo(new RoleDto() {
            Id = role1Id,
            Name = "Test Name 1",
        });
        listRolesResult.Items[1].Id.Should().Be(role2Id);
    }

    [Fact]
    public async Task ShouldUpdateRole()
    {
        var roleId = await HttpClient.CreateRole().CallAndParseResponse(new() {
        Name = "Test Name 1",
        });

        var updateResponse = await HttpClient.UpdateRole().Call(new() {
            Id = roleId,
        Name = "Updated Name 1",
        });

        await updateResponse.Should().HaveStatusCode(200);

        var updatedRole = await HttpClient.GetRoleById().CallAndParseResponse(roleId);

        updatedRole.Should().BeEquivalentTo(new RoleDto() {
            Id = roleId,
        Name = "Updated Name 1",
        });
    }

    [Fact]
    public async Task ShouldDeleteRole()
    {
        var roleId1 = await HttpClient.CreateRole().CallAndParseResponse(new() {
        Name = "Test Name 1",
        });
        var roleId2 = await HttpClient.CreateRole().CallAndParseResponse(new() {
        Name = "Test Name 2",
        });

        var deleteRoleResponse = await HttpClient.DeleteRole().Call(roleId1);

        await deleteRoleResponse.Should().HaveStatusCode(200);

        var listRolesResult = await HttpClient.ListRoles().CallAndParseResponse(new()
        {
            PageIndex = 1,
            PageSize = 5,
        });

        listRolesResult.Items.Should().HaveCount(1);
        listRolesResult.Items[0].Id.Should().Be(roleId2);
    }
}
