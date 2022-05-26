using AutoMapper;
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
        private readonly IMapper _mapper;
        public CpuMetricsAgentController(ILogger<CpuMetricsAgentController> logger, ICpuMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsAgentController");
            _repository = repository;
            _mapper = mapper;
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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetric, CpuMetricDto>());
            var mapper = config.CreateMapper();
            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            foreach (var metric in metrics)
            {
                // Добавляем объекты в ответ, используя маппер
                response.Metrics.Add(mapper.Map<CpuMetricDto>(metric));
            }
            return Ok(response);
        }

    }
}
