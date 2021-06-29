using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Infrastructure.Persistence.Configurations
{
    public class ProductFeaturesValueConfiguration : IEntityTypeConfiguration<ProductFeaturesValue>
    {
        public void Configure(EntityTypeBuilder<ProductFeaturesValue> builder)
        {
            builder.HasKey(e => e.ProductFeaturesValueId);


            builder.HasOne(e => e.Product)
               .WithMany(v => v.ProductFeaturesValues)
               .IsRequired(true)
               .HasForeignKey(e => e.ProductId);

            builder.HasOne(e => e.ProductFeature)
               .WithMany(v => v.ProductFeaturesValues)
               .IsRequired(true)
               .HasForeignKey(e => e.ProductFeatureId);

        }
    }
}
