namespace Testamente.Domain;

public interface IReportSectionRepository
{
	Task SaveCreateAsync(ReportSection section);
}