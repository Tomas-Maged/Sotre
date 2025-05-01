using Domian.Contercts;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data.Repository;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Persistence.Identity;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddDbContext<StoreDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("IdentyConnection")));
            services.AddScoped<IDbInitializer, DbInitializer>();
           services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepsitory, CacheRepositor>();
            services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) => 
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            }); 

            return services;
        }
        }
    }

