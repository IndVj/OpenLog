using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenLogAPI.Controllers
{
    [Authorize(Roles = "Logger")]
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        // GET: api/<LogController>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string level, string source)
        {
            var logs = await _logService.GetAllLogsAsync(level, source);
            return Ok(logs);
        }
     
        // POST api/<LogController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LogEntry entry)
        {
            await _logService.AddLogAsync(entry);
            return CreatedAtAction("Entry is added", new { id = entry.Id });
        }
     
    }
}
