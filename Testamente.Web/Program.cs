using Testamente.DataAccess;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//services.AddScoped<IReportSectionService, ReportSectionService>();
//services.AddScoped<IReportSectionRepo, ReportSectionRepo>();
//services.AddScoped<IReportSectionPostQuery, ReportSectionPostQuery>();
var connStr = builder.Configuration.GetValue<string>("DBCONNSTR");
var services = builder.Services;
services.AddDbContext<ReportSectionContext>();
//services.AddDbContext<ReportSectionContext>(options => options.UseSqlServer(connStr, b => b.MigrationsAssembly("Testamente.Web")));
//services.AddScoped<IDbConnectionProvider>(p => new DbConnectionProvider(connStr));
//services.AddScoped<IQueryExecutor, QueryExecutor>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
