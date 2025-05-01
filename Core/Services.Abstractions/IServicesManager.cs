using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
   public interface IServicesManager
    {
        IProudectServices ProudectServices { get; }
        ICacheService CacheService { get; }
        IBasketservice Basketservice { get; }
        IAuthService AuthService { get; }
    }
}
