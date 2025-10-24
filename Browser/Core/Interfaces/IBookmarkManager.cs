using Browser.Core.Models;

namespace Browser.Core.Interfaces
{
    public interface IBookmarkManager
    {
        Task<List<Bookmark>> LoadBookmarksAsync();
        Task CreateBookmarkAsync(string name, string url);
        Task DeleteBookmarkAsync(Guid id);
        Task EditBookmarkAsync(Bookmark updatedBookmark);
        List<Bookmark> GetAllBookmarks();
        Bookmark GetBookmarkByUrl(string url);
    }
}
