using Browser.Core.Models;

namespace Browser.Core.Interfaces
{
    public interface IHistoryManager
    {
        Task<List<History>> LoadHistoryAsync();

        // Method to add a new entry without duplication
        Task AddHistoryEntryAsync(string url, string title);

        // Synchronous retrieval for the UI
        List<History> GetAllHistory();
    }
}