﻿using Core.Models;

namespace Core.Interfaces.Services
{
    public interface ILogService
    {
        Task<string> AddLogAsync(LogEntry logEntry);
        Task<List<LogEntry>> GetAllLogsAsync(string level, string source);
    }
}