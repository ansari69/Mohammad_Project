using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasKey(e => e.ProductId);


            builder.HasOne(e => e.ProductType)
               .WithMany(v => v.Products)
               .IsRequired(true)
               .HasForeignKey(e => e.ProductTypeId);

            builder.HasOne(e => e.ProductPosition)
               .WithMany(v => v.Products)
               .IsRequired(true)
               .HasForeignKey(e => e.ProductPositionId);

            builder.HasOne(e => e.Creator)
               .WithMany(v => v.CreatedProducts)
               .IsRequired(true)
               .HasForeignKey(e => e.CreatorId);




        }
    }
}
