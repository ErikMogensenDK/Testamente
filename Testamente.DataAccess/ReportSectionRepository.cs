using Testamente.Domain;
using Microsoft.EntityFrameworkCore;

namespace Testamente.DataAccess;

public class ReportSectionRepo : IReportSectionRepository
{
	private readonly ReportSectionContext _context;

	public ReportSectionRepo(ReportSectionContext context)
	{
		_context = context;
	}

	public async Task SaveCreateAsync(ReportSection section)
	{
		if (section == null)
		{
			throw new ArgumentNullException(nameof(section));
		}

		var dbEntity = new ReportSectionEntity
		{
			ReportSectionEntityId = section.ReportSectionId,
			Title = section.Title,
			Body = section.Body
		};
		_context.ReportSections.Add(dbEntity);
		
		await _context.SaveChangesAsync();
	}
}

public class ReportSectionContext: DbContext
{
	public ReportSectionContext(DbContextOptions options) : base(options)
	{

	}

	public DbSet<ReportSectionEntity> ReportSections { get; set; }
}