using Core.Models;

namespace Core.Interfaces.Repo
{
    public interface ILogRepository
    {
        Task<string> AddLogAsync(LogEntry logEntry);
        Task<List<LogEntry>> GetAllLogsAsync(string level, string source);
    }
}