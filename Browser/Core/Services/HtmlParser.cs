using Browser.Core.Interfaces;
using Browser.Core.Models;
using HtmlAgilityPack;

namespace Browser.Core.Services
{
    public class HtmlParser : IHtmlParser
    {
        public void ParsePage(PageContent pageContent)
        {
            try
            {
                // Main parsing logic
                pageContent.title = ExtractTitle(pageContent.rawHtml);
                pageContent.links = ExtractLinks(pageContent.rawHtml);
            }
            catch (System.Exception ex)
            {
                pageContent.title = "";
                pageContent.links = new List<string>();
            }
        }

        private string ExtractTitle(string html)
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var titleNode = doc.DocumentNode.SelectSingleNode("//title");
            return titleNode != null
                 ? HtmlEntity.DeEntitize(titleNode.InnerText.Trim())
                 : "";
        }

        private List<string> ExtractLinks(string html)
        {
            var links = new List<string>();

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            // Select all <a> elements with href attributes
            var linkNodes = doc.DocumentNode.SelectNodes("//a[@href]");
            if (linkNodes != null)
            {
                foreach (var linkNode in linkNodes.Take(5)) // Take only first 5
                {
                    string href = linkNode.GetAttributeValue("href", "");
                    if (!string.IsNullOrEmpty(href))
                    {
                        // Decode HTML entities in the href attribute
                        string decodedHref = HtmlEntity.DeEntitize(href).Trim();
                        links.Add(decodedHref);
                    }
                }
            }
            return links;
        }

    }
}
