using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        [HttpGet("available/agent/{agentId}")]
        public IActionResult GetAvailableFromAgent([FromRoute] int agentId)
        {
            return Ok();
        }
        [HttpGet("available/cluster")]
        public IActionResult GetAvailableFromAllCluster()
        {
            return Ok();
        }
    }
}
