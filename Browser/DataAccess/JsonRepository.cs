using Browser.Core.Interfaces;
using System.Text.Json;

namespace Browser.DataAccess
{
    // JsonRepository implements the generic contract for any type T
    public class JsonRepository<T> : IDataRepository<T> where T : class
    {
        private readonly string filePath;

        // The file path is passed in the constructor
        public JsonRepository(string fileName)
        {
            // reliable path
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        public async Task<T> LoadAsync()
        {
            if (!File.Exists(filePath))
            {
                // If the file doesn't exist, return null so the Manager can initialize the default
                return null;
            }

            try
            {
                // Uses a stream to read data from the disk in chunks.
                using (var stream = File.OpenRead(filePath))
                {
                    // Deserializes directly from the stream, minimizing memory footprint.
                    return await JsonSerializer.DeserializeAsync<T>(stream);
                }
            }
            catch (JsonException)
            {
                // Handle corrupted JSON 
                return null;
            }
            catch (IOException)
            {
                // Handle file access errors
                return null;
            }
        }

        public async Task SaveAsync(T data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };

            try
            {
                // Uses a stream to write data to the disk in chunks
                using (var stream = File.Create(filePath))
                {
                    // Serializes the object directly to the stream
                    await JsonSerializer.SerializeAsync(stream, data, options);
                }
            }
            catch (IOException)
            {
                // Handle file access or write errors
            }
        }
    }
}

