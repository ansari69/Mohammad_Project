using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Infrastructure.Persistence.Configurations
{
    public class SelectedColorProductConfiguration : IEntityTypeConfiguration<SelectedColorProduct>
    {
        public void Configure(EntityTypeBuilder<SelectedColorProduct> builder)
        {
            builder.HasKey(e => e.SelectedColorProductId);

            builder.HasOne(e => e.ProductColor)
               .WithMany(v => v.SelectedColorProducts)
               .IsRequired(true)
               .HasForeignKey(e => e.ProductColorId);

            builder.HasOne(e => e.Product)
               .WithMany(v => v.SelectedColorProducts)
               .IsRequired(true)
               .HasForeignKey(e => e.ProductId);

        }
    }
}
