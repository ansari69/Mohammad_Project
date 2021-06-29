using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Infrastructure.Persistence.Configurations
{
    public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder.HasKey(e => e.ProductColorId);



        }
    }
}
