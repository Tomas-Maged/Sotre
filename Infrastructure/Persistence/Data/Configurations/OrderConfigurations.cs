using Domian.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<order>
    {
        public void Configure(EntityTypeBuilder<order> builder)
        {
            builder.OwnsOne(o => o.ShippingAdress, address => address.WithOwner());

             builder.HasMany(o => o.OrderItems)
                .WithOne()
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.deliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(o => o.PaymentStutas)
                .HasConversion(s => s.ToString(), s => Enum.Parse<OrderPaymentStutas>(s));

            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,4)");


        }
    }
}
