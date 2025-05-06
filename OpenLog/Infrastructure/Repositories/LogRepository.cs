using Core.Interfaces.Repo;
using Core.Models;
using Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IMongoCollection<LogEntryDto> _collection;

        public LogRepository(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoSettings:Connection"]);
            var database = client.GetDatabase(configuration["MongoSettings:Database"]);
            _collection = database.GetCollection<LogEntryDto>("LogEntries");
        }

        public async Task<string> AddLogAsync(LogEntry logEntry)
        {

            var logEntryDto = LogEntryDto.FromCore(logEntry);

            await _collection.InsertOneAsync(logEntryDto);

            return logEntryDto.Id;
        }

        public async Task<List<LogEntry>> GetAllLogsAsync(string level, string source)
        {
            var builder = Builders<LogEntryDto>.Filter;
            var filter = builder.Empty;

            if (!String.IsNullOrEmpty(level))
                filter &= builder.Eq(l => l.Level, level);

            if (!String.IsNullOrEmpty(source))
                filter &= builder.Eq(l => l.Source, source);

            var logEntrDtoList = await _collection.Find(filter).ToListAsync();

            return logEntrDtoList.Select(logEntryDto => logEntryDto.ToCore()).ToList();

        }

    }
}
