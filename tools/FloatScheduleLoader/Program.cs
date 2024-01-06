using CsvHelper;
using CsvHelper.Configuration;
using FloatScheduleLoader;
using FloatScheduleLoader.Dtos;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.RegularExpressions;

const string paginationQueryParams = "pageSize=1000&pageIndex=1";

const string filePath = "C:\\Users\\Matthew\\Documents\\Project planning.csv";
const string apiBaseUri = "https://localhost:7808/api";
const string floatScheduleEndpoint = apiBaseUri + "/float-schedule";
const string projectsEndpoint = apiBaseUri + "/projects?" + paginationQueryParams;
const string peopleEndpoint = apiBaseUri + "/persons?" + paginationQueryParams;
const string nonProjectTimeTypesEndpoint = apiBaseUri + "/nonprojecttimetypes?" + paginationQueryParams;

var httpClient = new HttpClient();

var allProjects = (await httpClient.GetFromJsonAsync<AppResponse<PaginatedListResponse<ProjectDto>>>(projectsEndpoint))?.Content
    ?? throw new Exception("Failed to fetch projects.");

var allPeople = (await httpClient.GetFromJsonAsync<AppResponse<PaginatedListResponse<PersonDto>>>(peopleEndpoint))?.Content
    ?? throw new Exception("Failed to fetch people.");

var allNonProjectTimeTypes = (await httpClient.GetFromJsonAsync<AppResponse<PaginatedListResponse<NonProjectTimeTypeDto>>>(nonProjectTimeTypesEndpoint))?.Content
    ?? throw new Exception("Failed to fetch non-project time types.");

var uploadRows = await ParseCsv(filePath);

foreach (var uploadRow in uploadRows)
{

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
        IDictionary<string, object> row = csv.GetRecord<dynamic>();
        string project = "";
        string timeOff = "";
        Dictionary<DateOnly, decimal> weeklyHours = new();

        row.TryGetValue("Name", out var nameRaw);
        string name = nameRaw as string;

        if (name == "SCHEDULED" || name == "CAPACITY" || string.IsNullOrEmpty(name))
        {
            continue;
        }

        foreach (var field in row)
        {
            if (field.Key == "Project")
            {
                project = field.Value as string;
                continue;
            }

            if (field.Key == "TimeOff")
            {
                timeOff = field.Value as string;
                continue;
            }

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