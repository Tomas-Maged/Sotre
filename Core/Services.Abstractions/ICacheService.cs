using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICacheService
    {
        Task SetCacheValeuAsncy(string key, object value, TimeSpan Duration);
        Task<string?> GetCachValeuAsync(string key);
    }
}
