using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetSingleProduct
{
    public class GetSingleProductVM
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDisplayName { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        // public string ImageAddress { get; set; }
        //  public string ImageName { get; set; }

        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }


        public string ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductPositionId { get; set; }
        public string ProductPositionName { get; set; }

        public IEnumerable<ProductColorDTO> Colors { get; set; }
        public IEnumerable<ProductFeaturesValueDTO> Features { get; set; }

    }

    public class ProductColorDTO
    {
        public string ProductColorId { get; set; }
        public string PersianName { get; set; }
        public string LatinName { get; set; }
        public string ColorCode { get; set; }
    }

    public class ProductFeaturesValueDTO
    {
        public string FeatureName { get; set; }
        public string FeaturesValue { get; set; }
    }



    

}
