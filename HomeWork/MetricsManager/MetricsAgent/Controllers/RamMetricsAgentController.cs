using MetricsAgent.DAL;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsAgentController : ControllerBase
    {
        private readonly ILogger<RamMetricsAgentController> _logger;
        private IRamMetricsRepository _repository;
        public RamMetricsAgentController(ILogger<RamMetricsAgentController> logger, IRamMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsAgentController");
            _repository = repository;
        }

        [HttpGet("available")]
        public IActionResult GetAvailable([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            IList<RamMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            if (metrics is null)
            {
                _logger.LogInformation("По запросу ничего не было найдено.");
                return NotFound();
            }
            else
            {
                _logger.LogInformation($"Метрики оперативной памяти с {fromTime} по {toTime} переданы");
                return Ok(metrics);
            }
        }
    }
}
