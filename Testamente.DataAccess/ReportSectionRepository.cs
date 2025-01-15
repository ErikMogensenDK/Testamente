using Testamente.Domain;

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
