using Browser.Core.Models;

namespace Browser.Core.Interfaces
{
    public interface IBrowserService
    {
        Task<PageContent> LoadPageAsync(string url);
    }
}
