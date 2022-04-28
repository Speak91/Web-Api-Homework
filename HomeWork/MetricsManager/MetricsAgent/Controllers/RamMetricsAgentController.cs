using AutoMapper;
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
        private readonly IMapper _mapper;

        public RamMetricsAgentController(ILogger<RamMetricsAgentController> logger, IRamMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsAgentController");
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("available")]
        public IActionResult GetAvailable([FromRoute] DateTime fromTime, [FromRoute] DateTime toTime)
        {
            IList<RamMetric> metrics = _repository.GetByTimePeriod(fromTime, toTime);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<RamMetric, RamMetricDto>());
            var mapper = config.CreateMapper();
            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricDto>()
            };
            foreach (var metric in metrics)
            {
                // Добавляем объекты в ответ, используя маппер
                response.Metrics.Add(mapper.Map<RamMetricDto>(metric));
            }
            return Ok(response);
        }
    }
}
