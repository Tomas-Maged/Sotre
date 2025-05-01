using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Contercts
{
    public interface ICacheRepsitory
    {
        Task SetAsync(string key, object value, TimeSpan Duration);
        Task<string?> GetAsync(string key);
    }
}
