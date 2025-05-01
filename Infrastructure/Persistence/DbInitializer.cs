using Domian.Contercts;
using Domian.Models;
using Domian.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
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
        private readonly 
            StoreDBContext _Context;
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            StoreDBContext Context,
            StoreIdentityDbContext IdentityDbContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _Context = Context;
            _identityDbContext = IdentityDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task InitializeIdentityAsync()
        {
            if (_identityDbContext.Database.GetPendingMigrations().Any())
            {
              await  _identityDbContext.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin",
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin",
                });

            }
                if (!_userManager.Users.Any())
            {
                var SuperAdminUser = new AppUser()
                {
                    DisplyName = "Super Admin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "0123456789",

                };
                var adminUser = new AppUser()
                {
                    DisplyName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "0123456789",
                };
              await  _userManager.CreateAsync(SuperAdminUser,"P@ssw0rd");
              await  _userManager.CreateAsync(adminUser,"P@ssw0rd");
                await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminUser, "Admin"); 
            }
             
        }
    }
}

