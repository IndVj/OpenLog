using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenLogAPI.Controllers
{
    [Authorize]
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
            if (string.IsNullOrWhiteSpace(level) || string.IsNullOrWhiteSpace(source))
            {
                return BadRequest("Both 'level' and 'source' query parameters are required.");
            }

            var logs = await _logService.GetAllLogsAsync(level, source);

            if (logs == null || !logs.Any())
            {
                return NotFound("No logs found for the given criteria.");
            }

            return Ok(logs);
        }
     
        // POST api/<LogController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] LogEntry entry)
        {
            if (entry == null)
            {
                return BadRequest("Log entry is required.");
            }


            var createdÍd = await _logService.AddLogAsync(entry);
            return Created("Entry is added", createdÍd);
        }
     
    }
}
