using System;
using System.Collections.Generic;

namespace MetricsAgent.DAL
{
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {
        IList<CpuMetric> GetByTimePeriod(DateTime toTime, DateTime fromTime );
    }
}
