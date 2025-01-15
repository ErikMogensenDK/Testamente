using HtmlAgilityPack;
using Testamente.App;
using Testamente.Domain;

namespace Testamente.DomainTests;

[TestClass]
public class ReportGeneratorTests
{
    [TestMethod]
    public void GenerateReport_GeneratesExpectedHtmlDocument()
    {
        // Arrange
        string reportTitle = "Autogenereret udkast til testamente";
        ReportSection sectionOne = new(){Title= "This is my Report Title", Body="This is my report body"};
        ReportSection sectionTwo = new(){Title= "This is my second report sub-title", Body="This is my second report body"};
        List<ReportSection> sections = new() { sectionOne, sectionTwo };

        // Act
        ReportGenerator reportGenerator = new();
        HtmlDocument actualDocument = reportGenerator.GenerateReport(reportTitle, sections);

        //Assert
        var actualH2Elements = actualDocument.DocumentNode.SelectNodes("//h2");
        var actualBodyElements = actualDocument.DocumentNode.SelectNodes("//div[@class='body-section']");
        Assert.AreEqual(sectionOne.Title, actualH2Elements[0].InnerHtml);
        Assert.AreEqual(sectionOne.Body, actualBodyElements[0].InnerHtml);
        Assert.AreEqual(sectionTwo.Title, actualH2Elements[1].InnerHtml);
        Assert.AreEqual(sectionTwo.Body, actualBodyElements[1].InnerHtml);

    }

    [DataRow(null, "123")]
    [DataRow(null, null)]
    [DataTestMethod]
    public void GenerateReport_ReturnsNullIfInvalidInput(List<ReportSection> sections, string reportTitle)
    {
        // Act
        ReportGenerator reportGenerator = new();
        HtmlDocument actualDocument = reportGenerator.GenerateReport(reportTitle, sections);

        //Assert
        Assert.IsTrue(actualDocument == null);
    }
    [TestMethod]
    public void GenerateReport_ReturnsNullIfNullSectionElement()
    {
        List<ReportSection> sections = new();
        string reportTitle = "123";
        // Act
        ReportGenerator reportGenerator = new();
        HtmlDocument actualDocument = reportGenerator.GenerateReport(reportTitle, sections);

        //Assert
        Assert.IsTrue(actualDocument == null);
    }

}