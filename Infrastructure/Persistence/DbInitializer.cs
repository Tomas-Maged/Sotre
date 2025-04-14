using Domian.Contercts;
using Domian.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDBContext _Context;
        public DbInitializer(StoreDBContext Context)
        {
            _Context = Context;
        }
        public async Task InitializeAsync()
        {
            if (_Context.Database.GetPendingMigrations().Any())
            {
                await _Context.Database.MigrateAsync();
            }

            if (!_Context.ProudectTypes.Any())
            {
                var FilePath = await File.ReadAllTextAsync(@"../Infrastructure\Persistence\Seeding\products.json");
                var productTypes = JsonSerializer.Deserialize<List<ProudectType>>(FilePath);
                if (productTypes != null && productTypes.Any())
                {
                    await _Context.ProudectTypes.AddRangeAsync(productTypes);
                    await _Context.SaveChangesAsync();
                }
            }

            if (!_Context.ProudectBrands.Any())
            {
                var FilePath = await File.ReadAllTextAsync(@"../Infrastructure\Persistence\Seeding\brands.json");
                var productBrands = JsonSerializer.Deserialize<List<ProudectBrand>>(FilePath);
                if (productBrands != null && productBrands.Any())
                {
                    await _Context.ProudectBrands.AddRangeAsync(productBrands);
                    await _Context.SaveChangesAsync();
                }
            }
            if (!_Context.Proudects.Any())
            {
                var FilePath = await File.ReadAllTextAsync(@"../Infrastructure\Persistence\Seeding\products.json");
                var products = JsonSerializer.Deserialize<List<Proudect>>(FilePath);
                if (products != null && products.Any())
                {
                    await _Context.Proudects.AddRangeAsync(products);
                    await _Context.SaveChangesAsync();
                }
            }

        }

    }
}

