using Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
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
            var memoryCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

            _distributedCache.SetString(key, value, memoryCacheEntryOptions);
        }
    }
}
