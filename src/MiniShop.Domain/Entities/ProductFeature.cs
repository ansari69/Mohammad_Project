using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Domain.Entities
{
   public class ProductFeature
    {
        public ProductFeature()
        {
            ProductFeaturesValues = new List<ProductFeaturesValue>();
        }


        public string ProductFeatureId { get; set; }
        public string FeatureName { get; set; }
        public bool IsActive { get; set; }

        #region Relations

        public IList<ProductFeaturesValue> ProductFeaturesValues { get; set; }


        #endregion




    }
}
