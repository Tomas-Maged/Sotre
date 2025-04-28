using Domian.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domian.Contercts
{
    public interface IBasketRepository 
    {
       Task<CustomerBasket?>  GetBaskedAsync(string Id);
       Task<CustomerBasket?>  UpdateBaskedAsync(CustomerBasket basket , TimeSpan? TimeToLive = null);
        Task<bool> DeleteBasketAsync(string Id);



    }
}
