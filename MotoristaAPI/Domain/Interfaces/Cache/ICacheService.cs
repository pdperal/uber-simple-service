using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Cache
{
    public interface IRedisService
    {
        void SetCache(string key, string value);
    }
}
