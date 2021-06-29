using Microsoft.AspNetCore.Identity;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Domain.Entities;
using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Application.Common.Helpers
{
   public class DataSeeding
    {

        public async static Task SeedBaseDataAsync(IAppDbContext context,
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {

            SeedProductStatus(context);
            SeedColor(context);
            SeedProductType(context);
            SeedProductFeature(context);

            await context.SaveChangesAsync();
        }


        private static void SeedProductStatus(IAppDbContext context)
        {
            var productPositions = new List<ProductPosition>
            {
                new ProductPosition { ProductPositionId = "1", ProductPositionName = "محصولات جدید", IsActive = true },
                new ProductPosition { ProductPositionId = "2", ProductPositionName = "محصولات برتر", IsActive = true }
            };

            foreach (var product in productPositions)
                context.ProductPositions.Add(product);
        }

        private static void SeedColor(IAppDbContext context)
        {
            var productColors = new List<ProductColor>
            {
                new ProductColor { ProductColorId = "1", PersianName = "قرمز", LatinName = "Red" , ColorCode= "#ff4d4d", IsActive = true },
                new ProductColor { ProductColorId = "2", PersianName = "مشکی", LatinName = "Black" , ColorCode= "#000000", IsActive = true },
                new ProductColor { ProductColorId = "3", PersianName = "سبز", LatinName = "Green" , ColorCode= "#009933", IsActive = true },
                new ProductColor { ProductColorId = "4", PersianName = "ابی", LatinName = "Blue" , ColorCode= "#005ce6", IsActive = true },

            };

            foreach (var color in productColors)
                context.ProductColors.Add(color);
        }

        private static void SeedProductType(IAppDbContext context)
        {
            var productTypes = new List<ProductType>
            {
                new ProductType { ProductTypeId = "1", ProductTypeName = "کالای دیجیتال", Description = "کالای دیجیتال" , IsActive = true },
                new ProductType { ProductTypeId = "2", ProductTypeName = "لوازم جانبی", Description = "لوازم جانبی" , IsActive = true },

            };

            foreach (var typeModel in productTypes)
                context.ProductTypes.Add(typeModel);
        }

        private static void SeedProductFeature(IAppDbContext context)
        {
            var productFeatures = new List<ProductFeature>
            {
                new ProductFeature { ProductFeatureId = "1", FeatureName = "شناسه کالا" , IsActive = true },
                new ProductFeature { ProductFeatureId = "2", FeatureName = "ظرفیت" , IsActive = true },
                new ProductFeature { ProductFeatureId = "3", FeatureName = "اندازه صفحه نمایش" , IsActive = true },
                new ProductFeature { ProductFeatureId = "4", FeatureName = "مدل پردازنده" , IsActive = true },
                new ProductFeature { ProductFeatureId = "5", FeatureName = "سازنده" , IsActive = true },

            };

            foreach (var featureModel in productFeatures)
                context.ProductFeatures.Add(featureModel);
        }

    }
}
