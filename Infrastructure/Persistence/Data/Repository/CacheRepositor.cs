using Domian.Contercts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Data.Repository
{
    public class CacheRepositor(IConnectionMultiplexer connection) : ICacheRepsitory
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? value : default;
        }

        public async Task SetAsync(string key, object value, TimeSpan Duration)
        {
          var RedisValue =  JsonSerializer.Serialize(value);
          await _database.StringSetAsync(key, RedisValue, Duration);

        }
    }
}

