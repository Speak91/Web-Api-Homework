﻿using AutoMapper;
using MetricsAgent.DAL;
using MetricsAgent.Responses;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Добавлять сопоставления в таком стиле надо для всех объектов
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<RamMetric, RamMetricDto>();
        }
    }
}
