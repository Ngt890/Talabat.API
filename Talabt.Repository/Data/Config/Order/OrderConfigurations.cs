using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;


namespace Talabat.Repository.Data.Config.Order
{
    public class OrderConfigurations : IEntityTypeConfiguration<Talabat.Core.Entities.OrderAggregate.Order>
    {
        public void Configure(EntityTypeBuilder<Talabat.Core.Entities.OrderAggregate.Order> builder)
        {
           builder.Property(O=>O.Status)
                .HasConversion(Ostatus=>Ostatus.ToString(),Ostatus=>(OrderStatus)Enum.Parse(typeof(OrderStatus),Ostatus));

            builder.Property(p => p.SubTotal)
                    .HasColumnType("decimal(18,2)");
            builder.OwnsOne(O => O.ShippingAddress, A => A.WithOwner());
            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
               .OnDelete(DeleteBehavior.NoAction);
                

        
        
        }
    }
}
