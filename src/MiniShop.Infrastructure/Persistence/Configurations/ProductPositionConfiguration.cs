using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Infrastructure.Persistence.Configurations
{
    public class ProductPositionConfiguration : IEntityTypeConfiguration<ProductPosition>
    {
        public void Configure(EntityTypeBuilder<ProductPosition> builder)
        {

            builder.HasKey(e => e.ProductPositionId);


        }
    }
}
