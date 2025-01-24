using Testamente.Domain;

namespace Testamente.DataAccess;

public class ReportSectionRepository : IReportSectionRepository
{
	private readonly TestamenteContext _context;

	public ReportSectionRepository(TestamenteContext context)
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
