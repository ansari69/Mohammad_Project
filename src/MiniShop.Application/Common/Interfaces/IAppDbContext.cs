using Microsoft.EntityFrameworkCore;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Common.Interfaces
{
   public interface IAppDbContext
    {

        #region Tables

         DbSet<Product> Products { get; set; }
         DbSet<ProductColor> ProductColors { get; set; }
         DbSet<ProductFeature> ProductFeatures { get; set; }
         DbSet<ProductFeaturesValue> ProductFeaturesValues { get; set; }
         DbSet<ProductLike> ProductLikes { get; set; }
         DbSet<ProductPosition> ProductPositions { get; set; }
         DbSet<ProductType> ProductTypes { get; set; }
         DbSet<SelectedColorProduct> SelectedColorProducts { get; set; }

        #endregion



        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());



    }
}
