﻿using Dapper;
using MetricsManager.DAL.Interfaces;
using MetricsManager.DAL.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.DAL.Repositories
{
    public class AgentsRepository : IAgentsRepository
    {
        private readonly ILogger<AgentsRepository> _logger;
        private const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public AgentsRepository(ILogger<AgentsRepository> logger)
        {
            _logger = logger;
        }
        public void Create(AgentInfo agent)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var count = connection.ExecuteScalar<int>($"SELECT Count(*) FROM agents WHERE uri=@uri;", new { uri = agent.Url });
                if (count > 0)
                {
                    throw new ArgumentException("Агент уже существует");
                }
                var result = connection.Execute(
                $"INSERT INTO agents (uri,isenabled) VALUES (@uri,@isenabled);",
                new 
                { 
                    uri = agent.Url, 
                    isenabled = agent.IsEnabled 
                }
                );
                if (result <= 0) throw new InvalidOperationException("Не удалось добавить агента.");
            }
        }

        public IList<AgentInfo> Get()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.Query<AgentInfo>($"SELECT * FROM agents").ToList();
            }
        }

        public AgentInfo GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<AgentInfo>($"SELECT * FROM agents WHERE id=@id",
                    new { id });
            }
        }

        public void Update(AgentInfo agent)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                var count = connection.ExecuteScalar<int>($"SELECT Count(*) FROM agents WHERE uri=@uri;", new { uri = agent.Url });
                if (count <= 0)
                {
                    throw new ArgumentException("Агент не существует");
                }
                var result = connection.Execute($"UPDATE agents SET uri=@uri, isenabled=@isenabled where id=@id;",
                new 
                { 
                    uri = agent.Url, 
                    isenabled = agent.IsEnabled,
                    id = agent.Id
                }
                );
                if (result <= 0) throw new InvalidOperationException("Не удалось обновить агента.");
            }
        }
    }
}
