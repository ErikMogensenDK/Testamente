using Testamente.DataAccess;
using Microsoft.EntityFrameworkCore;
using Testamente.Query;
using Testamente.Domain;
using Testamente.App.Services;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Testamente.Web;
using Testamente.Web.Identity;
using System.Security.Claims;
using Testamente.App;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


var connStr = builder.Configuration.GetValue<string>("DBCONNSTR");
services.AddDbContext<TestamenteContext>(options =>
{
    options.UseSqlServer(connStr, b => b.MigrationsAssembly("Testamente.Web"));
});
services.AddDbContext<IdentityContext>(options =>
{
    options.UseSqlServer(connStr, b => b.MigrationsAssembly("Testamente.Web"));
});
services.AddSingleton<IDbConnectionProvider> (p => new DbConnectionProvider(connStr));
services.AddSingleton<IQueryExecutor, QueryExecutor>();
services.AddSingleton<IPersonQuery, PersonQuery>();
services.AddSingleton<IPersonRepository, PersonRepository>();
services.AddTransient<IPersonService, PersonService>();
services.AddScoped<InheritanceCalculator>();
services.AddScoped<IdentityContext>();
SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
services.AddAuthorization();
//services.AddAuthentication().AddBearerToken(IdentityConstants.ApplicationScheme);
//services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
services.AddAuthentication().AddCookie("Identity.Bearer");
services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddApiEndpoints();
services.AddOpenApiDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("users/me", async (ClaimsPrincipal claims, IdentityContext context) => 
{
    string userId = claims.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

    return await context.Users.FindAsync(userId);
})
.RequireAuthorization();

app.MapIdentityApi<User>();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();

//services.AddScoped<IDbConnectionProvider>(p => new DbConnectionProvider(connStr));
//services.AddScoped<IQueryExecutor, QueryExecutor>();
//services.AddScoped<IReportSectionService, ReportSectionService>();
//services.AddScoped<IReportSectionRepo, ReportSectionRepo>();
//services.AddScoped<IReportSectionPostQuery, ReportSectionPostQuery>();
//services.AddDbContext<ReportSectionContext>();
//services.AddDbContext<ReportSectionContext>(options => options.UseSqlServer(connStr, b => b.MigrationsAssembly("Testamente.Web")));