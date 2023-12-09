using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Cache
{
    public class RedisService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public List<MotoristaCache> BuscarMotoristas()
        {
            var json = _distributedCache.GetString("motorista:localizacao");

            return JsonConvert.DeserializeObject<List<MotoristaCache>>(json);
        }
    }
}
