using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Infrastructure.Persistence.Configurations
{
   public class ApplicationRoleConfiguration
     : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.IsActive)
                .HasDefaultValue(true);


        }
    }
}
