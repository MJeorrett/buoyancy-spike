using BuoyancyApi.E2eTests.Shared.Dtos.Persons;
using BuoyancyApi.E2eTests.Shared.Endpoints;
using BuoyancyApi.E2eTests.Shared.Extensions;
using BuoyancyApi.E2eTests.Shared.WebApplicationFactory;
using FluentAssertions;
using Xunit.Abstractions;

namespace BuoyancyApi.E2eTests.Persons;

[Collection(CustomWebApplicationCollection.Name)]
public class PersonE2eTests : TestBase
{
    public PersonE2eTests(
        CustomWebApplicationFixture webApplicationFixture,
        ITestOutputHelper testOutputHelper) :
        base(webApplicationFixture, testOutputHelper)
    {
    }

    [Fact]
    public async Task ShouldReturnCreatedPersonById()
    {
        var createResponse = await HttpClient.CreatePerson().Call(new() {
        Name = "Test Name 1",
        RoleId = 1,
        });

        await createResponse.Should().HaveStatusCode(201);

        var createdPersonId = await createResponse.ReadResponseContentAs<int>();

        var getPersonByIdResponse = await HttpClient.GetPersonById().Call(createdPersonId);

        await getPersonByIdResponse.Should().HaveStatusCode(200);

        var returnedPerson = await getPersonByIdResponse.ReadResponseContentAs<PersonDto>();

        returnedPerson.Name.Should().Be("Test Name 1");
        returnedPerson.RoleId.Should().Be(1);
    }

    [Fact]
    public async Task ShouldListCreatedPersons()
    {
        var person1Id = await HttpClient.CreatePerson().CallAndParseResponse(new() {
        Name = "Test Name 1",
        RoleId = 1,
        });
        var person2Id = await HttpClient.CreatePerson().CallAndParseResponse(new() {
        Name = "Test Name 2",
        RoleId = 2,
        });

        var listPersonsResult = await HttpClient.ListPersons().CallAndParseResponse(new()
        {
            PageIndex = 1,
            PageSize = 5,
        });

        listPersonsResult.Items.Should().HaveCount(2);
        listPersonsResult.Items[0].Should().BeEquivalentTo(new PersonDto() {
            Id = person1Id,
            Name = "Test Name 1",
            RoleId = 1,
        });
        listPersonsResult.Items[1].Id.Should().Be(person2Id);
    }

    [Fact]
    public async Task ShouldUpdatePerson()
    {
        var personId = await HttpClient.CreatePerson().CallAndParseResponse(new() {
        Name = "Test Name 1",
        RoleId = 1,
        });

        var updateResponse = await HttpClient.UpdatePerson().Call(new() {
            Id = personId,
        Name = "Updated Name 1",
        RoleId = 3,
        });

        await updateResponse.Should().HaveStatusCode(200);

        var updatedPerson = await HttpClient.GetPersonById().CallAndParseResponse(personId);

        updatedPerson.Should().BeEquivalentTo(new PersonDto() {
            Id = personId,
        Name = "Updated Name 1",
        RoleId = 3,
        });
    }

    [Fact]
    public async Task ShouldDeletePerson()
    {
        var personId1 = await HttpClient.CreatePerson().CallAndParseResponse(new() {
        Name = "Test Name 1",
        RoleId = 1,
        });
        var personId2 = await HttpClient.CreatePerson().CallAndParseResponse(new() {
        Name = "Test Name 2",
        RoleId = 2,
        });

        var deletePersonResponse = await HttpClient.DeletePerson().Call(personId1);

        await deletePersonResponse.Should().HaveStatusCode(200);

        var listPersonsResult = await HttpClient.ListPersons().CallAndParseResponse(new()
        {
            PageIndex = 1,
            PageSize = 5,
        });

        listPersonsResult.Items.Should().HaveCount(1);
        listPersonsResult.Items[0].Id.Should().Be(personId2);
    }
}
