﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        public CpuMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent(
        [FromRoute] int agentId, 
        [FromRoute] TimeSpan fromTime, 
        [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Метрика от агента {agentId} c {fromTime} по {toTime}");
            return Ok();
        }

        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAgent(
        [FromRoute] int agentId, 
        [FromRoute] TimeSpan fromTime,
        [FromRoute] TimeSpan toTime, 
        [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Метрика от агента {agentId} c {fromTime} по {toTime} со следующими " +
                $"данными {percentile}");

            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"Метрика от кластера c {fromTime} по {toTime}");

            return Ok();
        }

        [HttpGet("cluster/from/{fromTime}/to/{toTime}/percentiles/{percentile}")]
        public IActionResult GetMetricsByPercentileFromAllCluster(
        [FromRoute] TimeSpan fromTime, 
        [FromRoute] TimeSpan toTime,
        [FromRoute] Percentile percentile)
        {
            _logger.LogInformation($"Метрика от кластера c {fromTime} по {toTime} со следующими данными {percentile}");
            return Ok();
        }
    }
}