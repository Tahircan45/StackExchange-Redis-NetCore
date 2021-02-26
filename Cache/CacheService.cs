using StackExchange_Redis_NetCore.Cache.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace StackExchange_Redis_NetCore.Cache
{
    
    public class CacheService : ICacheService
    {
        private RedisServer _redisServer;
        public CacheService(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public void add(string key, object data)
        {
            string jsonData = JsonSerializer.Serialize(data);
            _redisServer.Database.StringSet(key, jsonData,TimeSpan.FromSeconds(30));
        }
        public T get<T>(string key)
        {
            if (Any(key))
            {
                string jsondata=_redisServer.Database.StringGet(key);
                return JsonSerializer.Deserialize<T>(jsondata);
            }
            return default;
        }

        public void remove(string key)
        {
            _redisServer.Database.KeyDelete(key);
        }
        public bool Any(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public void clear()
        {
            _redisServer.FlushDatabase();
        }

    }
}
