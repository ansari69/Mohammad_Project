using MiniShop.Application.Common.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetForInsertProduct
{
   public class GetForInsertProductVM
    {
        public IEnumerable<ProductColorVM> ProductColors  { get; set; }
        public IEnumerable<ProductFeatureVM> ProductFeatures { get; set; }
        public IEnumerable<ProductPositionVM> ProductPositions { get; set; }
        public IEnumerable<ProductTypeVM> ProductTypes { get; set; }
    }
}
