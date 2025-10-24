namespace Browser.Core.Models
{
    public class Bookmark
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public override string ToString()
        {
            return $"{name} - {url}";
        }
    }
}
