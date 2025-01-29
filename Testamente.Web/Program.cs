using Testamente.DataAccess;
using Microsoft.EntityFrameworkCore;
using Testamente.Query;
using Testamente.Domain;
using Testamente.App.Services;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Testamente.Web;
using Testamente.Web.Identity;

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
services.AddScoped<IDbConnectionProvider> (p => new DbConnectionProvider(connStr));
services.AddScoped<IQueryExecutor, QueryExecutor>();
services.AddScoped<IPersonQuery, PersonQuery>();
services.AddScoped<IPersonRepository, PersonRepository>();
services.AddScoped<IPersonService, PersonService>();
SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
services.AddAuthorization();
services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);
services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<IdentityContext>()
    .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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