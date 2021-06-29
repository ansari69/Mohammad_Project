using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Domain.Entities
{
   public class ProductFeaturesValue
    {
        public ProductFeaturesValue()
        {

        }

        public string ProductFeaturesValueId { get; set; }
        public string FeaturesValue { get; set; }
        public bool IsActive { get; set; }


        #region Relations

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string ProductFeatureId { get; set; }
        public ProductFeature ProductFeature { get; set; }


        #endregion

    }
}
