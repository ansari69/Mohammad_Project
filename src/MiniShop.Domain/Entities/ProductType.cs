using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Domain.Entities
{
   public class ProductType
    {
        public ProductType()
        {
            Products = new List<Product>();
        }

        public string ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }


        #region Relations

        public IList<Product> Products { get; set; }

        #endregion


    }
}
