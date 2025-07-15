using Domian.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class orderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(o => o.Proudect, proudectInOrderItem => proudectInOrderItem.WithOwner());

            builder.Property(o => o.Price)
                .HasColumnType("decimal(18,4)");



        }
    }
}
