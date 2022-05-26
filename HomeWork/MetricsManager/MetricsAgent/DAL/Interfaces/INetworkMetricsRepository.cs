using System;
using System.Collections.Generic;

namespace MetricsAgent.DAL
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetric>
    {
        IList<NetworkMetric> GetByTimePeriod(DateTime toTime, DateTime fromTime);
    }
}
