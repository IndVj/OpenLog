using Core.Interfaces.Repo;
using Core.Interfaces.Services;
using Core.Models;

namespace Core.Services
{
    public class LogService : ILogService
    {
        public readonly ILogRepository logRepository;

        public LogService(ILogRepository logRepository)
        {
            this.logRepository = logRepository;
        }

        public async Task<List<LogEntry>> GetAllLogsAsync(string level, string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentException("Source cannot be empty");
            }

            if (string.IsNullOrEmpty(level))
            {
                throw new ArgumentException("Level cannot be empty");

            }

            return await this.logRepository.GetAllLogsAsync(level, source);

        }
        public async Task<string> AddLogAsync(LogEntry logEntry)
        {
            if (string.IsNullOrEmpty(logEntry.Source))
            {
                throw new ArgumentException("Source cannot be empty");
            }

            if (string.IsNullOrEmpty(logEntry.Level))
            {
                throw new ArgumentException("Level cannot be empty");
            }

            if (string.IsNullOrEmpty(logEntry.Message))
            {
                throw new ArgumentException("Message cannot be empty");
            }

            return await this.logRepository.AddLogAsync(logEntry);

        }
    }
}
