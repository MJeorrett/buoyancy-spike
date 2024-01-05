using BuoyancyApi.Application;
using BuoyancyApi.Infrastructure;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

#region addServices

services.AddControllers();

services.AddApplication();
services.AddInfrastructure(builder.Configuration);

var allowedOrigins = configuration.GetSection("CorsOrigins").Get<string[]>() ?? Array.Empty<string>();

services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder
        .WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod()
        .Build());
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

#endregion

var app = builder.Build();

#region configurePipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion

public partial class Program { }