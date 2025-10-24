namespace Browser.Core.Interfaces
{
    public interface IDataRepository<T> where T : class
    {
        // Loads data from file. Returns null or a default if not found.
        Task<T> LoadAsync();

        // Saves the data, overwriting the file entry.
        Task SaveAsync(T data);
    }
}
