using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.DAL
{
    public class RamMetricsRepository : IRamMetricsRepository
    {
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeHandler());
        }

        public void Create(RamMetric item)
        {
            throw new NotImplementedException();
        }

        public IList<RamMetric> GetByTimePeriod(DateTime fromTime, DateTime toTime)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                // Читаем, используя Query, и в шаблон подставляем тип данных,
                // объект которого Dapper, он сам заполнит его поля
                // в соответствии с названиями колонок
                return connection.Query<RamMetric>("SELECT * FROM cpumetrics WHERE (time >= @from) and (time <= @to)", new
                { from = fromTime, to = toTime }).ToList();
            }
        }
    }
}
