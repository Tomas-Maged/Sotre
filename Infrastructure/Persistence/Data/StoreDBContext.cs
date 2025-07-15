using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domian.Models;
using Domian.Models.OrderModels;
namespace Persistence.Data
{
   public class StoreDBContext : DbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReferance).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<order> orders { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Proudect> Proudects { get; set; }
        public DbSet<ProudectType> ProudectTypes { get; set; }
        public DbSet<ProudectBrand> ProudectBrands { get; set; }
    }
    
  }

