using Browser.Core.Models;

namespace Browser.Core.Interfaces
{
    public interface IHomePageManager
    {
        Task<HomePageSetting> LoadHomePageAsync();
        Task SaveHomePageAsync(string url);
    }
}
