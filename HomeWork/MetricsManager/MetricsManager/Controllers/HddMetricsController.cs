using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        public HddMetricsController(ILogger<HddMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
        }

        [HttpGet("left/agent/{agentId}")]
        public IActionResult GetFreeHDDSpaceFromAgent([FromRoute] int agentId)
        {
            _logger.LogInformation($"Список свободного места от агента {agentId} передан");
            return Ok();
        }

        [HttpGet("left/cluster")]
        public IActionResult GetFreeHDDSpaceFromAllCluster()
        {
            _logger.LogInformation($"Список свободного места от кластера передан");
            return Ok();
        }
    }
}
