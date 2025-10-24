namespace Browser.Core.Models
{
    public class PageContent
    {
        public string url { get; set; }
        public string rawHtml { get; set; }
        public string title { get; set; }
        public string statusCode { get; set; }
        public List<string> links { get; set; }

        public PageContent(string url)
        {
            this.url = url;
            this.links = new List<string>();
        }

    }
}
