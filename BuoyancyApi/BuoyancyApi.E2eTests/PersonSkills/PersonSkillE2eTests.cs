using BuoyancyApi.E2eTests.Shared.Dtos.PersonSkills;
using BuoyancyApi.E2eTests.Shared.Endpoints;
using BuoyancyApi.E2eTests.Shared.Extensions;
using BuoyancyApi.E2eTests.Shared.WebApplicationFactory;
using FluentAssertions;
using Xunit.Abstractions;

namespace BuoyancyApi.E2eTests.PersonSkills;

[Collection(CustomWebApplicationCollection.Name)]
public class PersonSkillE2eTests : TestBase
{
    public PersonSkillE2eTests(
        CustomWebApplicationFixture webApplicationFixture,
        ITestOutputHelper testOutputHelper) :
        base(webApplicationFixture, testOutputHelper)
    {
    }

    [Fact]
    public async Task ShouldReturnCreatedPersonSkillById()
    {
        var createResponse = await HttpClient.CreatePersonSkill().Call(new() {
        PersonId = 1,
        SkillId = 1,
        });

        await createResponse.Should().HaveStatusCode(201);

        var createdPersonSkillId = await createResponse.ReadResponseContentAs<int>();

        var getPersonSkillByIdResponse = await HttpClient.GetPersonSkillById().Call(createdPersonSkillId);

        await getPersonSkillByIdResponse.Should().HaveStatusCode(200);

        var returnedPersonSkill = await getPersonSkillByIdResponse.ReadResponseContentAs<PersonSkillDto>();

        returnedPersonSkill.PersonId.Should().Be(1);
        returnedPersonSkill.SkillId.Should().Be(1);
    }

    [Fact]
    public async Task ShouldListCreatedPersonSkills()
    {
        var personSkill1Id = await HttpClient.CreatePersonSkill().CallAndParseResponse(new() {
        PersonId = 1,
        SkillId = 1,
        });
        var personSkill2Id = await HttpClient.CreatePersonSkill().CallAndParseResponse(new() {
        PersonId = 2,
        SkillId = 2,
        });

        var listPersonSkillsResult = await HttpClient.ListPersonSkills().CallAndParseResponse(new()
        {
            PageIndex = 1,
            PageSize = 5,
        });

        listPersonSkillsResult.Items.Should().HaveCount(2);
        listPersonSkillsResult.Items[0].Should().BeEquivalentTo(new PersonSkillDto() {
            Id = personSkill1Id,
            PersonId = 1,
            SkillId = 1,
        });
        listPersonSkillsResult.Items[1].Id.Should().Be(personSkill2Id);
    }

    [Fact]
    public async Task ShouldUpdatePersonSkill()
    {
        var personSkillId = await HttpClient.CreatePersonSkill().CallAndParseResponse(new() {
        PersonId = 1,
        SkillId = 1,
        });

        var updateResponse = await HttpClient.UpdatePersonSkill().Call(new() {
            Id = personSkillId,
        PersonId = 3,
        SkillId = 3,
        });

        await updateResponse.Should().HaveStatusCode(200);

        var updatedPersonSkill = await HttpClient.GetPersonSkillById().CallAndParseResponse(personSkillId);

        updatedPersonSkill.Should().BeEquivalentTo(new PersonSkillDto() {
            Id = personSkillId,
        PersonId = 3,
        SkillId = 3,
        });
    }

    [Fact]
    public async Task ShouldDeletePersonSkill()
    {
        var personSkillId1 = await HttpClient.CreatePersonSkill().CallAndParseResponse(new() {
        PersonId = 1,
        SkillId = 1,
        });
        var personSkillId2 = await HttpClient.CreatePersonSkill().CallAndParseResponse(new() {
        PersonId = 2,
        SkillId = 2,
        });

        var deletePersonSkillResponse = await HttpClient.DeletePersonSkill().Call(personSkillId1);

        await deletePersonSkillResponse.Should().HaveStatusCode(200);

        var listPersonSkillsResult = await HttpClient.ListPersonSkills().CallAndParseResponse(new()
        {
            PageIndex = 1,
            PageSize = 5,
        });

        listPersonSkillsResult.Items.Should().HaveCount(1);
        listPersonSkillsResult.Items[0].Id.Should().Be(personSkillId2);
    }
}
