using Browser.Core.Interfaces;
using Browser.Core.Models;

namespace Browser.Core.Services
{
    public class HttpRequester : IHttpRequester
    {
        private static readonly HttpClient httpClient = new HttpClient();
        public async Task<PageContent> GetRawHtmlAsync(string url)
        {
            var pageContent = new PageContent(url);

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                string html = await response.Content.ReadAsStringAsync();

                pageContent.rawHtml = html;
                pageContent.statusCode = $"{(int)response.StatusCode} {response.ReasonPhrase}";
            }
            catch (HttpRequestException hre)
            {
                pageContent.rawHtml = $"Error: {hre.Message}";
                pageContent.statusCode = "Network Error";
            }
            catch (Exception ex)
            {
                pageContent.rawHtml = $"Unexpected error: {ex.Message}";
                pageContent.statusCode = "Error";
            }

            return pageContent;
        }

    }
}
