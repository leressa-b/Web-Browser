using Browser.Core.Interfaces;
using Browser.Core.Models;

namespace Browser.Core.Services
{
    public class HistoryManager : IHistoryManager
    {
        private readonly IDataRepository<List<History>> repository;

        // The persistent List<History> structure
        private List<History> history;

        public HistoryManager(IDataRepository<List<History>> repository)
        {
            this.repository = repository;
            // Synchronously load history on manager creation
            history = LoadHistoryAsync().GetAwaiter().GetResult();
        }

        public async Task<List<History>> LoadHistoryAsync()
        {
            var historyList = await repository.LoadAsync();

            // Returns the loaded list or a new empty list if the file is missing/empty
            if (historyList == null)
            {
                historyList = new List<History>();
            }

            return historyList;
        }

        public async Task AddHistoryEntryAsync(string url, string title)
        {
            // 1. Eliminate consecutive duplicate URLs
            // History is ordered newest to oldest.
            if (history.Count > 0 && history[0].url.Equals(url, StringComparison.OrdinalIgnoreCase))
            {
                // If duplicate found : update the timestamp of the last entry
                history[0].timestamp = DateTime.Now;
                await repository.SaveAsync(history);
                return;
            }

            // 2. Create the new entry
            var newEntry = new History
            {
                id = Guid.NewGuid(),
                url = url,
                title = title,
                timestamp = DateTime.Now
            };

            // 3. Add to start (most recent entry)
            history.Insert(0, newEntry);

            // 4. Persist the updated list to the file
            await repository.SaveAsync(history);
        }

        public List<History> GetAllHistory()
        {
            // Returns a copy of the list
            return history.ToList();
        }
    }
}