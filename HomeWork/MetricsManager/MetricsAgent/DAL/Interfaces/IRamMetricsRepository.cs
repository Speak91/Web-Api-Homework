using System;
using System.Collections.Generic;

namespace MetricsAgent.DAL
{
    public interface IRamMetricsRepository : IRepository<RamMetric>
    {
        IList<RamMetric> GetByTimePeriod(DateTime toTime, DateTime fromTime);
    }
}
