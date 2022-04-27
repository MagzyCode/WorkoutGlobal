using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WorkoutGlobal.Api.DatabaseContext;
using WorkoutGlobal.Api.Extensions;
using WorkoutGlobal.Api.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHealthChecks();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSqlContext(builder.Configuration);
//builder.Services.AddSingleton<IConfiguration, ConfigurationManager>();
builder.Services.ConfigureRepositories();
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseConnectionHealthCheck>(nameof(DatabaseConnectionHealthCheck))
    .AddCheck<ApiWorkHealthCheck>(nameof(ApiWorkHealthCheck));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
