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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsAgentController : ControllerBase
    {
        private readonly ILogger<HddMetricsAgentController> _logger;
        private IHddMetricsRepository _repository;
        public HddMetricsAgentController(ILogger<HddMetricsAgentController> logger, IHddMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
            _logger.LogDebug(1, "NLog встроен в HddMetricsAgentController");
        }

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetFreeHDDSpace([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            IList<HddMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            if (metrics is null)
            {
                _logger.LogInformation("По запросу ничего не было найдено.");
                return NotFound();
            }
            else
            {
                _logger.LogInformation($"Метрики свободного места с {fromTime} по {toTime} переданы");
                return Ok(metrics);
            }
        }
    }
}
