using BuoyancyApi.E2eTests.Shared.Dtos.Skills;
using BuoyancyApi.E2eTests.Shared.Endpoints;
using BuoyancyApi.E2eTests.Shared.Extensions;
using BuoyancyApi.E2eTests.Shared.WebApplicationFactory;
using FluentAssertions;
using Xunit.Abstractions;

namespace BuoyancyApi.E2eTests.Skills;

[Collection(CustomWebApplicationCollection.Name)]
public class SkillE2eTests : TestBase
{
    public SkillE2eTests(
        CustomWebApplicationFixture webApplicationFixture,
        ITestOutputHelper testOutputHelper) :
        base(webApplicationFixture, testOutputHelper)
    {
    }

    [Fact]
    public async Task ShouldReturnCreatedSkillById()
    {
        var createResponse = await HttpClient.CreateSkill().Call(new() {
        Name = "Test Name 1",
        });

        await createResponse.Should().HaveStatusCode(201);

        var createdSkillId = await createResponse.ReadResponseContentAs<int>();

        var getSkillByIdResponse = await HttpClient.GetSkillById().Call(createdSkillId);

        await getSkillByIdResponse.Should().HaveStatusCode(200);

        var returnedSkill = await getSkillByIdResponse.ReadResponseContentAs<SkillDto>();

        returnedSkill.Name.Should().Be("Test Name 1");
    }

    [Fact]
    public async Task ShouldListCreatedSkills()
    {
        var skill1Id = await HttpClient.CreateSkill().CallAndParseResponse(new() {
        Name = "Test Name 1",
        });
        var skill2Id = await HttpClient.CreateSkill().CallAndParseResponse(new() {
        Name = "Test Name 2",
        });

        var listSkillsResult = await HttpClient.ListSkills().CallAndParseResponse(new()
        {
            PageIndex = 1,
            PageSize = 5,
        });

        listSkillsResult.Items.Should().HaveCount(2);
        listSkillsResult.Items[0].Should().BeEquivalentTo(new SkillDto() {
            Id = skill1Id,
            Name = "Test Name 1",
        });
        listSkillsResult.Items[1].Id.Should().Be(skill2Id);
    }

    [Fact]
    public async Task ShouldUpdateSkill()
    {
        var skillId = await HttpClient.CreateSkill().CallAndParseResponse(new() {
        Name = "Test Name 1",
        });

        var updateResponse = await HttpClient.UpdateSkill().Call(new() {
            Id = skillId,
        Name = "Updated Name 1",
        });

        await updateResponse.Should().HaveStatusCode(200);

        var updatedSkill = await HttpClient.GetSkillById().CallAndParseResponse(skillId);

        updatedSkill.Should().BeEquivalentTo(new SkillDto() {
            Id = skillId,
        Name = "Updated Name 1",
        });
    }

    [Fact]
    public async Task ShouldDeleteSkill()
    {
        var skillId1 = await HttpClient.CreateSkill().CallAndParseResponse(new() {
        Name = "Test Name 1",
        });
        var skillId2 = await HttpClient.CreateSkill().CallAndParseResponse(new() {
        Name = "Test Name 2",
        });

        var deleteSkillResponse = await HttpClient.DeleteSkill().Call(skillId1);

        await deleteSkillResponse.Should().HaveStatusCode(200);

        var listSkillsResult = await HttpClient.ListSkills().CallAndParseResponse(new()
        {
            PageIndex = 1,
            PageSize = 5,
        });

        listSkillsResult.Items.Should().HaveCount(1);
        listSkillsResult.Items[0].Id.Should().Be(skillId2);
    }
}
