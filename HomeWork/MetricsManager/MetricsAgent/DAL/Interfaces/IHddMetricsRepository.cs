using System;
using System.Collections.Generic;

namespace MetricsAgent.DAL
{
    public interface IHddMetricsRepository : IRepository<HddMetric>
    {
        IList<HddMetric> GetByTimePeriod(DateTime toTime, DateTime fromTime);
    }
}
