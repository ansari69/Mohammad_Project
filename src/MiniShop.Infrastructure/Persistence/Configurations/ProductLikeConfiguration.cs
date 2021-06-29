using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Infrastructure.Persistence.Configurations
{
    public class ProductLikeConfiguration : IEntityTypeConfiguration<ProductLike>
    {
        public void Configure(EntityTypeBuilder<ProductLike> builder)
        {
            builder.HasKey(e => e.ProductLikeId);


            builder.HasOne(e => e.Product)
               .WithMany(v => v.ProductLikes)
               .IsRequired(true)
               .HasForeignKey(e => e.ProductId);

            builder.HasOne(e => e.Creator)
              .WithMany(v => v.CreatedProductLikes)
              .IsRequired(true)
              .HasForeignKey(e => e.CreatorId);



        }
    }
}
