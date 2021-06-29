using MiniShop.Application.Common.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductFeatures.Queries.GetAllProductFeatures
{
   public class GetAllProductFeaturesVM
    {
        public int PageId { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public IEnumerable<ProductFeatureVM> ProductFeatures { get; set; }
    }
}
