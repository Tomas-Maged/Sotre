using Domian.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    class ProudectConfigurations : IEntityTypeConfiguration<Proudect>
    {
        public void Configure(EntityTypeBuilder<Proudect> builder)
        {
            builder.HasOne(p => p.ProudectType)
                .WithMany()
                .HasForeignKey(p => p.TypeId);

            builder.HasOne(p => p.ProudectBrand)
                .WithMany()
                .HasForeignKey(p => p.BrandId);

            builder.Property(p => p.Price).HasConversion<decimal>()
                .HasColumnType("decimal(18,2)");
        }
    }
}

