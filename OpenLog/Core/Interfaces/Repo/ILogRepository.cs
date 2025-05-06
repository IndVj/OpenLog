using Core.Models;

namespace Core.Interfaces.Repo
{
    public interface ILogRepository
    {
        Task AddLogAsync(LogEntry logEntry);
        Task<List<LogEntry>> GetAllLogsAsync(string level, string source);
    }
}