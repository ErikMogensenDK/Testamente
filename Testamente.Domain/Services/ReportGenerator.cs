using HtmlAgilityPack;

namespace Testamente.Domain;

public class ReportGenerator
{
    public HtmlDocument GenerateReport(string reportTitle, List<ReportSection> sections)
    {
		if (sections == null || sections.Count == 0)
			return null;

		if (string.IsNullOrEmpty(reportTitle))
			return null;

		foreach (var section in sections)
		{
			if (string.IsNullOrEmpty(section.Title) || string.IsNullOrEmpty(section.Body))
				return null;
		}

		// Create the basic structure
		HtmlDocument document = new HtmlDocument();
        HtmlNode htmlNode = document.CreateElement("html");
		document = AddMainTitle(reportTitle, htmlNode, document);

		foreach (var section in sections)
		{
			document = AddSectionToHtmlReport(section, htmlNode, document);
		}

        // Append head and body to html
		return document;
    }

	private HtmlDocument AddSectionToHtmlReport(ReportSection section, HtmlNode node, HtmlDocument htmlDocument)
	{

		// Create sub-title
		HtmlNode h2Element = htmlDocument.CreateElement("h2");
		h2Element.SetAttributeValue("class", "sub-title");
		h2Element.InnerHtml = section.Title;
		node.AppendChild(h2Element);

		// Create body section
		HtmlNode htmlSection = htmlDocument.CreateElement("div");
		htmlSection.SetAttributeValue("class", "body-section");
		htmlSection.InnerHtml = section.Body;
		node.AppendChild(htmlSection);

		return htmlDocument;
	}

    private HtmlDocument AddMainTitle(string reportTitle, HtmlNode html, HtmlDocument document)
    {
		HtmlNode head = document.CreateElement("h1");
		head.SetAttributeValue("class", "main-title");
		head.InnerHtml = reportTitle;
		html.AppendChild(head);

		document.DocumentNode.AppendChild(html);

		return document;
	}
}
