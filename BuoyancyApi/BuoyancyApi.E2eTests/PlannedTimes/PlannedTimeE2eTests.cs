using BuoyancyApi.E2eTests.Shared.Dtos.PlannedTimes;
using BuoyancyApi.E2eTests.Shared.Endpoints;
using BuoyancyApi.E2eTests.Shared.Extensions;
using BuoyancyApi.E2eTests.Shared.WebApplicationFactory;
using FluentAssertions;
using Xunit.Abstractions;

namespace BuoyancyApi.E2eTests.PlannedTimes;

[Collection(CustomWebApplicationCollection.Name)]
public class PlannedTimeE2eTests : TestBase
{
    public PlannedTimeE2eTests(
        CustomWebApplicationFixture webApplicationFixture,
        ITestOutputHelper testOutputHelper) :
        base(webApplicationFixture, testOutputHelper)
    {
    }

    [Fact]
    public async Task ShouldReturnCreatedPlannedTimeById()
    {
        var createResponse = await HttpClient.CreatePlannedTime().Call(new() {
        ProjectId = 1,
        PersonId = 1,
        NonProjectTimeTypeId = 1,
        WeekStartingMonday = 1,
        Hours = 1,
        });

        await createResponse.Should().HaveStatusCode(201);

        var createdPlannedTimeId = await createResponse.ReadResponseContentAs<int>();

        var getPlannedTimeByIdResponse = await HttpClient.GetPlannedTimeById().Call(createdPlannedTimeId);

        await getPlannedTimeByIdResponse.Should().HaveStatusCode(200);

        var returnedPlannedTime = await getPlannedTimeByIdResponse.ReadResponseContentAs<PlannedTimeDto>();

        returnedPlannedTime.ProjectId.Should().Be(1);
        returnedPlannedTime.PersonId.Should().Be(1);
        returnedPlannedTime.NonProjectTimeTypeId.Should().Be(1);
        returnedPlannedTime.WeekStartingMonday.Should().Be(1);
        returnedPlannedTime.Hours.Should().Be(1);
    }

    [Fact]
    public async Task ShouldListCreatedPlannedTimes()
    {
        var plannedTime1Id = await HttpClient.CreatePlannedTime().CallAndParseResponse(new() {
        ProjectId = 1,
        PersonId = 1,
        NonProjectTimeTypeId = 1,
        WeekStartingMonday = 1,
        Hours = 1,
        });
        var plannedTime2Id = await HttpClient.CreatePlannedTime().CallAndParseResponse(new() {
        ProjectId = 2,
        PersonId = 2,
        NonProjectTimeTypeId = 2,
        WeekStartingMonday = 2,
        Hours = 2,
        });

        var listPlannedTimesResult = await HttpClient.ListPlannedTimes().CallAndParseResponse(new()
        {
            PageIndex = 1,
            PageSize = 5,
        });

        listPlannedTimesResult.Items.Should().HaveCount(2);
        listPlannedTimesResult.Items[0].Should().BeEquivalentTo(new PlannedTimeDto() {
            Id = plannedTime1Id,
            ProjectId = 1,
            PersonId = 1,
            NonProjectTimeTypeId = 1,
            WeekStartingMonday = 1,
            Hours = 1,
        });
        listPlannedTimesResult.Items[1].Id.Should().Be(plannedTime2Id);
    }

    [Fact]
    public async Task ShouldUpdatePlannedTime()
    {
        var plannedTimeId = await HttpClient.CreatePlannedTime().CallAndParseResponse(new() {
        ProjectId = 1,
        PersonId = 1,
        NonProjectTimeTypeId = 1,
        WeekStartingMonday = 1,
        Hours = 1,
        });

        var updateResponse = await HttpClient.UpdatePlannedTime().Call(new() {
            Id = plannedTimeId,
        ProjectId = 3,
        PersonId = 3,
        NonProjectTimeTypeId = 3,
        WeekStartingMonday = 3,
        Hours = 3,
        });

        await updateResponse.Should().HaveStatusCode(200);

        var updatedPlannedTime = await HttpClient.GetPlannedTimeById().CallAndParseResponse(plannedTimeId);

        updatedPlannedTime.Should().BeEquivalentTo(new PlannedTimeDto() {
            Id = plannedTimeId,
        ProjectId = 3,
        PersonId = 3,
        NonProjectTimeTypeId = 3,
        WeekStartingMonday = 3,
        Hours = 3,
        });
    }

    [Fact]
    public async Task ShouldDeletePlannedTime()
    {
        var plannedTimeId1 = await HttpClient.CreatePlannedTime().CallAndParseResponse(new() {
        ProjectId = 1,
        PersonId = 1,
        NonProjectTimeTypeId = 1,
        WeekStartingMonday = 1,
        Hours = 1,
        });
        var plannedTimeId2 = await HttpClient.CreatePlannedTime().CallAndParseResponse(new() {
        ProjectId = 2,
        PersonId = 2,
        NonProjectTimeTypeId = 2,
        WeekStartingMonday = 2,
        Hours = 2,
        });

        var deletePlannedTimeResponse = await HttpClient.DeletePlannedTime().Call(plannedTimeId1);

        await deletePlannedTimeResponse.Should().HaveStatusCode(200);

        var listPlannedTimesResult = await HttpClient.ListPlannedTimes().CallAndParseResponse(new()
        {
            PageIndex = 1,
            PageSize = 5,
        });

        listPlannedTimesResult.Items.Should().HaveCount(1);
        listPlannedTimesResult.Items[0].Id.Should().Be(plannedTimeId2);
    }
}
