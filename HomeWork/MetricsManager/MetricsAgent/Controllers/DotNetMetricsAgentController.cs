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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsAgentController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsAgentController> _logger;
        private IDotNetMetricsRepository _repository;

        public DotNetMetricsAgentController(ILogger<DotNetMetricsAgentController> logger, IDotNetMetricsRepository repository)
        {
            _logger = logger;
            _repository = repository;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsAgentController");
        }

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetErrorsCount([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            IList<DotNetMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
           
            if (metrics is null)
            {
                _logger.LogInformation("По запросу ничего не было найдено.");
                return NotFound(metrics);
            }
            else
            {
                _logger.LogInformation($"Метрики с {fromTime} по {toTime}");
                return Ok(metrics);
            }
        }
    }
}
