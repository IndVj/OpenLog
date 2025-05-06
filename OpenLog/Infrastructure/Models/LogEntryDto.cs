using Core.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Infrastructure.Models
{
    public class LogEntryDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = default!;

        public string Source { get; set; } = string.Empty;
        public string Level { get; set; } = "Info";
        public string Message { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;

        public static LogEntryDto FromCore(LogEntry core) => new LogEntryDto
        {
            Source = core.Source,
            Level = core.Level,
            Message = core.Message,
            TimeStamp = core.TimeStamp
        };

        public LogEntry ToCore() => new LogEntry
        {
            Id = Id,
            Source = Source,
            Level = Level,
            Message = Message,
            TimeStamp = TimeStamp
        };
    }
}
