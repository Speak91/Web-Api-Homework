using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsAgentController : ControllerBase
    {
        private readonly ILogger<CpuMetricsAgentController> _logger;
        public CpuMetricsAgentController(ILogger<CpuMetricsAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsAgentController");
        }

        [HttpGet("from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetrics(
        [FromRoute] TimeSpan fromTime, 
        [FromRoute] TimeSpan toTime, 
        [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Метрика c {fromTime} по {toTime} со следующими данными {percentile} передана");
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics( [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Метрика c {fromTime} по {toTime} передана");
            return Ok();
        }
    }
}
