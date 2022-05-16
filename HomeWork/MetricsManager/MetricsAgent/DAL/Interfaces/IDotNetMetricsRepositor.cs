using System;
using System.Collections.Generic;

namespace MetricsAgent.DAL
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetric>
    {
        IList<DotNetMetric> GetByTimePeriod(DateTime toTime, DateTime fromTime);
    }
}
