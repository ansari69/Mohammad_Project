using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Domain.Entities
{
   public class Product
    {
        public Product()
        {
            ProductLikes = new List<ProductLike>();

            SelectedColorProducts = new List<SelectedColorProduct>();

            ProductFeaturesValues = new List<ProductFeaturesValue>();
        }


      public string ProductId { get; set; }
      public string ProductName  { get; set; }
      public string ProductDisplayName { get; set; }

      public string Description { get; set; }
      public DateTime CreateDate { get; set; }
      public int Count { get; set; }

      public int Price { get; set; }
      public string ImageAddress { get; set; }
      public string ImageName { get; set; }

      public bool IsConfirm { get; set; }
      public bool IsActive { get; set; }



        #region Relations

        public string ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

        public string ProductPositionId { get; set; }
        public ProductPosition ProductPosition { get; set; }

        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }


        public IList<ProductLike> ProductLikes { get; set; }
        public IList<SelectedColorProduct> SelectedColorProducts { get; set; }
        public IList<ProductFeaturesValue> ProductFeaturesValues { get; set; }

        #endregion

    }
}
