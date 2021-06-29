using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Models.ViewModels
{
   public class ProductFeatureVM
    {
        public string ProductFeatureId { get; set; }
        public string FeatureName { get; set; }
        public bool IsActive { get; set; }
    }
}
