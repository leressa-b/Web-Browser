using Browser.Core.Interfaces;
using Browser.Core.Models;

namespace Browser.Core.Services
{
    public class BrowserService : IBrowserService
    {
        private readonly IHttpRequester httpRequester;
        private readonly IHtmlParser htmlParser;

        public BrowserService(IHttpRequester httpRequester, IHtmlParser htmlParser)
        {
            this.httpRequester = httpRequester;
            this.htmlParser = htmlParser;
        }

        public async Task<PageContent> LoadPageAsync(string url)
        {
            PageContent page = await httpRequester.GetRawHtmlAsync(url);
            htmlParser.ParsePage(page);

            return page;
        }
    }
}
