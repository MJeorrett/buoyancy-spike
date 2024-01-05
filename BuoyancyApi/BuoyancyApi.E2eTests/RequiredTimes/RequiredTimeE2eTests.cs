using BuoyancyApi.E2eTests.Shared.Dtos.RequiredTimes;
using BuoyancyApi.E2eTests.Shared.Endpoints;
using BuoyancyApi.E2eTests.Shared.Extensions;
using BuoyancyApi.E2eTests.Shared.WebApplicationFactory;
using FluentAssertions;
using Xunit.Abstractions;

namespace BuoyancyApi.E2eTests.RequiredTimes;

[Collection(CustomWebApplicationCollection.Name)]
public class RequiredTimeE2eTests : TestBase
{
    public RequiredTimeE2eTests(
        CustomWebApplicationFixture webApplicationFixture,
        ITestOutputHelper testOutputHelper) :
        base(webApplicationFixture, testOutputHelper)
    {
    }

    [Fact]
    public async Task ShouldReturnCreatedRequiredTimeById()
    {
        var createResponse = await HttpClient.CreateRequiredTime().Call(new() {
        ProjectId = 1,
        RoleId = 1,
        SkillId = 1,
        WeekStartingMonday = 1,
        Hours = 1,
        });

        await createResponse.Should().HaveStatusCode(201);

        var createdRequiredTimeId = await createResponse.ReadResponseContentAs<int>();

        var getRequiredTimeByIdResponse = await HttpClient.GetRequiredTimeById().Call(createdRequiredTimeId);

        await getRequiredTimeByIdResponse.Should().HaveStatusCode(200);

        var returnedRequiredTime = await getRequiredTimeByIdResponse.ReadResponseContentAs<RequiredTimeDto>();

        returnedRequiredTime.ProjectId.Should().Be(1);
        returnedRequiredTime.RoleId.Should().Be(1);
        returnedRequiredTime.SkillId.Should().Be(1);
        returnedRequiredTime.WeekStartingMonday.Should().Be(1);
        returnedRequiredTime.Hours.Should().Be(1);
    }

    [Fact]
    public async Task ShouldListCreatedRequiredTimes()
    {
        var requiredTime1Id = await HttpClient.CreateRequiredTime().CallAndParseResponse(new() {
        ProjectId = 1,
        RoleId = 1,
        SkillId = 1,
        WeekStartingMonday = 1,
        Hours = 1,
        });
        var requiredTime2Id = await HttpClient.CreateRequiredTime().CallAndParseResponse(new() {
        ProjectId = 2,
        RoleId = 2,
        SkillId = 2,
        WeekStartingMonday = 2,
        Hours = 2,
        });

        var listRequiredTimesResult = await HttpClient.ListRequiredTimes().CallAndParseResponse(new()
        {
            PageIndex = 1,
            PageSize = 5,
        });

        listRequiredTimesResult.Items.Should().HaveCount(2);
        listRequiredTimesResult.Items[0].Should().BeEquivalentTo(new RequiredTimeDto() {
            Id = requiredTime1Id,
            ProjectId = 1,
            RoleId = 1,
            SkillId = 1,
            WeekStartingMonday = 1,
            Hours = 1,
        });
        listRequiredTimesResult.Items[1].Id.Should().Be(requiredTime2Id);
    }

    [Fact]
    public async Task ShouldUpdateRequiredTime()
    {
        var requiredTimeId = await HttpClient.CreateRequiredTime().CallAndParseResponse(new() {
        ProjectId = 1,
        RoleId = 1,
        SkillId = 1,
        WeekStartingMonday = 1,
        Hours = 1,
        });

        var updateResponse = await HttpClient.UpdateRequiredTime().Call(new() {
            Id = requiredTimeId,
        ProjectId = 3,
        RoleId = 3,
        SkillId = 3,
        WeekStartingMonday = 3,
        Hours = 3,
        });

        await updateResponse.Should().HaveStatusCode(200);

        var updatedRequiredTime = await HttpClient.GetRequiredTimeById().CallAndParseResponse(requiredTimeId);

        updatedRequiredTime.Should().BeEquivalentTo(new RequiredTimeDto() {
            Id = requiredTimeId,
        ProjectId = 3,
        RoleId = 3,
        SkillId = 3,
        WeekStartingMonday = 3,
        Hours = 3,
        });
    }

    [Fact]
    public async Task ShouldDeleteRequiredTime()
    {
        var requiredTimeId1 = await HttpClient.CreateRequiredTime().CallAndParseResponse(new() {
        ProjectId = 1,
        RoleId = 1,
        SkillId = 1,
        WeekStartingMonday = 1,
        Hours = 1,
        });
        var requiredTimeId2 = await HttpClient.CreateRequiredTime().CallAndParseResponse(new() {
        ProjectId = 2,
        RoleId = 2,
        SkillId = 2,
        WeekStartingMonday = 2,
        Hours = 2,
        });

        var deleteRequiredTimeResponse = await HttpClient.DeleteRequiredTime().Call(requiredTimeId1);

        await deleteRequiredTimeResponse.Should().HaveStatusCode(200);

        var listRequiredTimesResult = await HttpClient.ListRequiredTimes().CallAndParseResponse(new()
        {
            PageIndex = 1,
            PageSize = 5,
        });

        listRequiredTimesResult.Items.Should().HaveCount(1);
        listRequiredTimesResult.Items[0].Id.Should().Be(requiredTimeId2);
    }
}
