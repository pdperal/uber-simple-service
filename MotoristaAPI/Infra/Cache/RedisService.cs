using Domain.Entities;
using Domain.Interfaces.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Cache
{
    public class RedisService : IRedisService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public void SetCache(string key, string value)
        {
            _distributedCache.SetString($"motoristas:{key}", value);
        }
    }
}
