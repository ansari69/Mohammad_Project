using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Domain.Entities
{
   public class ProductPosition
    {
        public ProductPosition()
        {
            Products = new List<Product>();
        }


        public string ProductPositionId { get; set; }
        public string ProductPositionName { get; set; }
        public bool IsActive { get; set; }


        #region Relations
        public IList<Product> Products { get; set; }

        #endregion

    }
}
