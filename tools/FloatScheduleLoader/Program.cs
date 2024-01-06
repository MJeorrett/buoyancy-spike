using CsvHelper;
using CsvHelper.Configuration;
using FloatScheduleLoader;
using FloatScheduleLoader.Dtos;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

const string paginationQueryParams = "?pageSize=1000&pageIndex=1";

const string filePath = "C:\\Users\\Matthew\\Documents\\Project planning.csv";
const string apiBaseUri = "https://localhost:7808/api";
const string plannedTimeEndpointFormat = apiBaseUri + "/projects/{0}/plannedtimes/{1}";
const string projectsEndpoint = apiBaseUri + "/projects";
const string peopleEndpoint = apiBaseUri + "/persons";
const string nonProjectTimeTypesEndpoint = apiBaseUri + "/nonprojecttimetypes";

var httpClient = new HttpClient();

var allProjects = (await httpClient.GetFromJsonAsync<AppResponse<PaginatedListResponse<ProjectDto>>>(projectsEndpoint + paginationQueryParams))?.Content?.Items
    ?? throw new Exception("Failed to fetch projects.");

var allPeople = (await httpClient.GetFromJsonAsync<AppResponse<PaginatedListResponse<PersonDto>>>(peopleEndpoint + paginationQueryParams))?.Content?.Items
    ?? throw new Exception("Failed to fetch people.");

var allNonProjectTimeTypes = (await httpClient.GetFromJsonAsync<AppResponse<PaginatedListResponse<NonProjectTimeTypeDto>>>(nonProjectTimeTypesEndpoint + paginationQueryParams))?.Content?.Items
    ?? throw new Exception("Failed to fetch non-project time types.");

var uploadRows = await ParseCsv(filePath);

foreach (var uploadRow in uploadRows)
{
    var person = allPeople.FirstOrDefault(_ => _.Name == uploadRow.Name);

    if (person is null)
    {
        throw new Exception($"Could not find person {uploadRow.Name}.");
    }

    var project = allProjects.FirstOrDefault(_ => _.Title == uploadRow.Project);

    if (project is null)
    {
        throw new Exception($"Could not find project {uploadRow.Project}.");
    }

    var nonProjectTimeOff = string.IsNullOrEmpty(uploadRow.TimeOff) ? null : allNonProjectTimeTypes.FirstOrDefault(_ => _.Name == uploadRow.TimeOff);

    foreach (var weekHours in uploadRow.WeeklyHours)
    {
        var dto = new UpsertPlannedTimeDto()
        {
            PersonId = person.Id,
            Hours = weekHours.Value,
            NonProjectTimeTypeId = nonProjectTimeOff?.Id,
        };

        var url = string.Format(plannedTimeEndpointFormat, project.Id, weekHours.Key.ToString("dd-MMM-yy"));

        var response = await httpClient.PutAsJsonAsync(url, dto);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to upload row. Received status {response.StatusCode} uploading row: {uploadRow}.");
        }
    }
}

static async Task<UploadRow[]> ParseCsv(string filePath)
{
    var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        NewLine = "\r\n",
    };

    var reader = new StreamReader(filePath);
    var csv = new CsvReader(reader, csvConfig);
    await csv.ReadAsync();
    csv.ReadHeader();

    var uploadRows = new List<UploadRow>();

    while (await csv.ReadAsync())
    {
        IDictionary<string, object> row = csv.GetRecord<dynamic>()!;
        Dictionary<DateOnly, decimal> weeklyHours = new();

        row.TryGetValue("Name", out var nameRaw);
        row.TryGetValue("Project", out var projectRaw);
        row.TryGetValue("TimeOff", out var timeOffRaw);
        string? name = nameRaw as string;
        string? project = projectRaw as string;
        string? timeOff = timeOffRaw as string;

        if (name == "SCHEDULED" || name == "CAPACITY" || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(project))
        {
            continue;
        }

        foreach (var field in row)
        {
            if (Regex.IsMatch(field.Key, "^[0-9][1-9]-[A-Z][a-z]{2}-[0-9][1-9]$"))
            {
                weeklyHours.Add(DateOnly.Parse(field.Key), decimal.Parse(field.Value as string));
            }
        }

        uploadRows.Add(new()
        {
            Name = name,
            Project = project,
            TimeOff = timeOff,
            WeeklyHours = weeklyHours,
        });
    }

    return uploadRows.ToArray();
}