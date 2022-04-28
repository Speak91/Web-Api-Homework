using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL
{
    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }
        public void Create(NetworkMetric item)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var result = connection.Execute(
                $"INSERT INTO networkmetrics(Time,Value) VALUES (@Time,@Value);",
                    new
                    {
                        Time = item.Time,
                        Value = item.Value,
                    }
                );
                if (result <= 0) throw new InvalidOperationException("Не удалось добавить метрику.");
            }
        }

        public IList<NetworkMetric> GetByTimePeriod(DateTime fromTime, DateTime toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                // Читаем, используя Query, и в шаблон подставляем тип данных,
                // объект которого Dapper, он сам заполнит его поля
                // в соответствии с названиями колонок
                return connection.Query<NetworkMetric>("SELECT * FROM cpumetrics WHERE (time >= @from) and (time <= @to)", new
                { from = fromTime, to = toTime }).ToList();
            }
        }
    }
}
