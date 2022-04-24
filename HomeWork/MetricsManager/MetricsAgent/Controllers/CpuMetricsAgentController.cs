using MetricsAgent.DAL;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsAgentController : ControllerBase
    {
        private readonly ILogger<CpuMetricsAgentController> _logger;
        private ICpuMetricsRepository _repository;

        public CpuMetricsAgentController(ILogger<CpuMetricsAgentController> logger, ICpuMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsAgentController");
            _repository = repository;
        }
      
        [HttpPost("create")]
        public IActionResult Create(CpuMetricCreateRequest request)
        {
            _repository.Create(new CpuMetric {Time = request.Time, Value = request.Value});
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetByTimePeriod([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            IList<CpuMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);

            if (metrics is null )
            {
                _logger.LogInformation("По запросу ничего не было найдено.");
                return NotFound();
            }
            else
            {
                _logger.LogInformation($"Метрики с {fromTime} по {toTime}");
                return Ok(metrics);
            }    
        }

    }
}
