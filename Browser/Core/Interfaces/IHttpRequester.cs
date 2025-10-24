using Browser.Core.Models;

namespace Browser.Core.Interfaces
{
    public interface IHttpRequester
    {
        Task<PageContent> GetRawHtmlAsync(string url);
    }
}
