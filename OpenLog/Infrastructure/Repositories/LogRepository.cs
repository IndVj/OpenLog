using Core.Interfaces.Repo;
using Core.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IMongoCollection<LogEntry> _collection;

        public LogRepository(IConfiguration configuration) 
        {
            var client = new MongoClient(configuration["MongoSettings:Connection"]);
            var database = client.GetDatabase(configuration["MongoSettings:Database"]);
            _collection = database.GetCollection<LogEntry>("LogEntries");        
        }

        public async Task AddLogAsync(LogEntry logEntry)
        {
           await _collection.InsertOneAsync(logEntry);
        }

        public async Task<List<LogEntry>> GetAllLogsAsync(string level,string source)
        {
            var builder = Builders<LogEntry>.Filter;
            var filter = builder.Empty;

            if (!String.IsNullOrEmpty(level))
                filter &= builder.Eq(l => l.Level, level);

            if (!String.IsNullOrEmpty(source))
                filter &= builder.Eq(source, source);

            return await _collection.Find(filter).ToListAsync();

        }

    } 
}
