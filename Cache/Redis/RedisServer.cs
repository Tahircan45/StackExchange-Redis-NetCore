using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using System;

namespace StackExchange_Redis_NetCore.Cache.Redis
{
    public class RedisServer
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private string configurationString;
        private int _currentDatabaseId = 0;
        public RedisServer(IConfiguration configuration)
        {
            CreateRedisConfigirationString(configuration);
            _connectionMultiplexer = ConnectionMultiplexer.Connect(configurationString);
            _database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);
        }
        public IDatabase Database => _database;
        private void CreateRedisConfigirationString(IConfiguration configuration)
        {
            string host = "127.0.0.1";
            string port = "6379";
            configurationString = $"{host}:{port}";

        }
        public void FlushDatabase()
        {
            _connectionMultiplexer.GetServer(configurationString).FlushDatabase(_currentDatabaseId);
        }
    }
}
