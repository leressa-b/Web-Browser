namespace Browser.Core.Models
{
    public class History
    {
        public Guid id { get; set; }
        public string url { get; set; }
        public DateTime timestamp { get; set; }

        public string title { get; set; }
        public override string ToString()
        {
            string displayName = string.IsNullOrEmpty(title) ? url : title;
            return $"{displayName} ({url}) - {timestamp.ToShortDateString()} {timestamp.ToShortTimeString()}";
        }
    }
}
