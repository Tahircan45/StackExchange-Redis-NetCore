using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackExchange_Redis_NetCore.Cache
{
    public interface ICacheService
    {
        T get<T>(string key);
        void add(string key, object data);
        void remove(string key);
        void clear();
        bool Any(string key);
    }
}
