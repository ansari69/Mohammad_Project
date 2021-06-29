using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Infrastructure.Persistence.Configurations
{
  public class ApplicationUserConfiguration
    : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.IsActive)
                .HasDefaultValue(true);

            builder.Property(e => e.FirstName)
                .HasDefaultValue("");

            builder.Property(e => e.LastName)
                .HasDefaultValue("");

            builder.Property(e => e.MelliCode)
                .IsRequired(false);
        }
    }
}
