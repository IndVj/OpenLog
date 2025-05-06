namespace Core.Models
{
    public class LogEntry
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Source { get; set; } = string.Empty;
        public string Level { get; set; } = "Info";
        public string Message { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
    }
}
