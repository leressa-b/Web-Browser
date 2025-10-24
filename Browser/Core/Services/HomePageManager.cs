using Browser.Core.Interfaces;
using Browser.Core.Models;

namespace Browser.Core.Services
{
    public class HomePageManager : IHomePageManager
    {
        private readonly IDataRepository<HomePageSetting> repository;

        public HomePageManager(IDataRepository<HomePageSetting> repository)
        {
            this.repository = repository;
        }

        public async Task<HomePageSetting> LoadHomePageAsync()
        {
            // 1. Load the settings from the repository (the file)
            var homePageSetting = await repository.LoadAsync();

            // 2. If the repository returns null, create the homepagw url with the default value
            if (homePageSetting == null || string.IsNullOrEmpty(homePageSetting.url))
            {
                homePageSetting = new HomePageSetting { url = "https://www.hw.ac.uk" };

            }

            return homePageSetting;
        }

        public async Task SaveHomePageAsync(string url)
        {
            // Fulfills the create and edit requirement by saving the new setting
            var homePageSetting = new HomePageSetting { url = url };
            await repository.SaveAsync(homePageSetting);
        }
    }
}
