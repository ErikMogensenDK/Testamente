using Microsoft.EntityFrameworkCore;

namespace Testamente.DataAccess;

public class ReportSectionContext : DbContext
{
	public ReportSectionContext(DbContextOptions<ReportSectionContext> options) : base(options)
	{

	}

	public DbSet<ReportSectionEntity> ReportSections { get; set; }
}