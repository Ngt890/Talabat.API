using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Config
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.Description)
                .IsRequired();
            builder.Property(p=>p.PictureUrl)
                .IsRequired();
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
            builder.HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p=>p.BrandId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p=>p.CategoryId)
              .OnDelete(DeleteBehavior.Cascade);









        }
    }
}
