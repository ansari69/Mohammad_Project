using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Domain.Entities;
using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Infrastructure.Persistence
{
   public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IAppDbContext
    {


        #region Tables

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductFeaturesValue> ProductFeaturesValues { get; set; }
        public DbSet<ProductLike> ProductLikes { get; set; }
        public DbSet<ProductPosition> ProductPositions { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<SelectedColorProduct> SelectedColorProducts { get; set; }


        #endregion



        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //getting configuratons for entities that are being executed
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var rel in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                rel.DeleteBehavior = DeleteBehavior.NoAction;
            }

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }


    }
}
