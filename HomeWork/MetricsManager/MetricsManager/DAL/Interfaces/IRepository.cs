using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByTimePeriod(DateTime from, DateTime to);
        IList<T> GetByTimePeriod(DateTime from, DateTime to, int agentId);

        void Create(T item);
        DateTime GetAgentLastMetricDate(int agentId);
    }
}
