using Domian.Contercts;
using Domian.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data.Repository
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database= connection.GetDatabase();
        public async Task<CustomerBasket?> GetBaskedAsync(string Id)
        {
          var redisValue = await  _database.StringGetAsync(Id);
            if (redisValue.IsNullOrEmpty) return null;
            var basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue);
            if (basket is null) return null;
            return basket;
        }
        public async Task<CustomerBasket?> UpdateBaskedAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var redisValue = JsonSerializer.Serialize(basket);
           var Flag = await _database.StringSetAsync(basket.Id, redisValue, TimeSpan.FromDays(30));
            return Flag ? await GetBaskedAsync(basket.Id) :null;
        }
        public async Task<bool> DeleteBasketAsync(string Id)
        {
            return await _database.KeyDeleteAsync(Id);
        }


    }
}
