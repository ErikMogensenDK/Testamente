using HtmlAgilityPack;
using Testamente.Domain;
namespace Testamente.App;

public interface IReportGenerator
{
    public HtmlDocument GenerateReport(string reportTitle, List<ReportSection> sections);
}