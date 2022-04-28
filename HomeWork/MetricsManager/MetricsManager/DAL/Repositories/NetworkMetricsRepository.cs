using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SQLite;

namespace MetricsManager.DAL.Repositories
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private readonly ILogger<NetworkMetricsRepository> _logger;
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public NetworkMetricsRepository(ILogger<NetworkMetricsRepository> logger)
        {
            _logger = logger;
        }
        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var result = connection.Execute(
                $"INSERT INTO networkmetrics (agentId,time,value) VALUES (@agentId,@time,@value);",
                    new
                    {
                        AgentId = item.AgentId,
                        Time = item.Time,
                        Value = item.Value,
                    }
                );
                if (result <= 0) throw new InvalidOperationException("Не удалось добавить метрику.");
            }
        }

        public DateTime GetAgentLastMetricDate(int agentId)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                //var commandParameters = new { from = fromSeconds, to = toSeconds };
                var result = connection.ExecuteScalar<long>("SELECT Max(time) FROM networkmetrics WHERE agentId = @agentId",
                                                     new { agentId });
                if (result >= 0)
                {
                    return Convert.ToDateTime(result);
                }
                throw new InvalidOperationException("Не удалось получить дату последней метрики");
            }
        }

        public IList<NetworkMetric> GetByTimePeriod(DateTime from, DateTime to)
        {
            var fromSeconds = from.ToUniversalTime();
            var toSeconds = to.ToUniversalTime();
            if (fromSeconds > toSeconds) return new List<NetworkMetric>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var commandParameters = new { from = fromSeconds, to = toSeconds };
                return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE (time >= @from) and (time <= @to)",
                                                    commandParameters).ToList();
            }
        }

        public IList<NetworkMetric> GetByTimePeriod(DateTime from, DateTime to, int agentId)
        {
            var fromSeconds = from.ToUniversalTime();
            var toSeconds = to.ToUniversalTime();
            if (fromSeconds > toSeconds) return new List<NetworkMetric>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var commandParameters = new { from = fromSeconds, to = toSeconds };
                return connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE (agentId = @agentId) (time >= @from) and (time <= @to)",
                                                    commandParameters).ToList();
            }
        }
    }
}
