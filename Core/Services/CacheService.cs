using Domian.Contercts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CacheService(ICacheRepsitory cacheRepsitory) : ICacheService
    {
        public Task<string?> GetCachValeuAsync(string key)
        {
           var Value =  cacheRepsitory.GetAsync(key);
            return Value == null ? null : Value;
        }

        public async Task SetCacheValeuAsncy(string key, object value, TimeSpan Duration)
        {
           await cacheRepsitory.SetAsync(key, value, Duration);
        }
    }
}
