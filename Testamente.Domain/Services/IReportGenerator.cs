using HtmlAgilityPack;

namespace Testamente.Domain;

public interface IReportGenerator
{
    public HtmlDocument GenerateReport(string reportTitle, List<ReportSection> sections);
}