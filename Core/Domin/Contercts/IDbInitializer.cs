using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Contercts
{
   public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}
