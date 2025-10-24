using Browser.Core.Interfaces;
using Browser.Core.Models;

namespace Browser.Core.Services
{
    public class BookmarkManager : IBookmarkManager
    {
        private readonly IDataRepository<List<Bookmark>> repository;
        private List<Bookmark> bookmarks;
        public BookmarkManager(IDataRepository<List<Bookmark>> repository)
        {
            this.repository = repository;
            bookmarks = LoadBookmarksAsync().GetAwaiter().GetResult();
        }
        public async Task<List<Bookmark>> LoadBookmarksAsync()
        {
            // 1. Load the bookmarks from the repository (the file)
            var bookmarkList = await repository.LoadAsync();

            // 2. If the repository returns null we create the bookmark with the default value
            if (bookmarkList == null)
            {
                bookmarkList = new List<Bookmark>();
            }

            return bookmarkList;
        }

        public async Task CreateBookmarkAsync(string name, string url)
        {
            // Simple check to avoid duplicates based on the unique URL
            if (bookmarks.Any(b => b.url == url))
            {
                return;
            }

            var newBookmark = new Bookmark
            {
                id = Guid.NewGuid(),
                name = name,
                url = url
            };

            // 1. Add to the list
            bookmarks.Add(newBookmark);

            // 2. Persist the updated list to the file
            await repository.SaveAsync(bookmarks);
        }

        public async Task DeleteBookmarkAsync(Guid id)
        {
            // 1. Find and remove the item from the in memory list
            int count = bookmarks.RemoveAll(b => b.id == id);

            // 2. Only save if something was actually removed
            if (count > 0)
            {
                await repository.SaveAsync(bookmarks);
            }
        }

        public async Task EditBookmarkAsync(Bookmark updatedBookmark)
        {
            // 1. Find the index using the ID
            int index = bookmarks.FindIndex(b => b.id == updatedBookmark.id);

            if (index != -1)
            {
                // 2. Replace the old object with the new one
                bookmarks[index] = updatedBookmark;

                // 3. Persist the updated list
                await repository.SaveAsync(bookmarks);
            }
        }
        public List<Bookmark> GetAllBookmarks()
        {
            // Returns a copy of the list to prevent external modification
            return bookmarks.ToList();
        }
        public Bookmark GetBookmarkByUrl(string url)
        {
            return bookmarks.FirstOrDefault(b => b.url.Equals(url, StringComparison.OrdinalIgnoreCase));
        }
    }
}
