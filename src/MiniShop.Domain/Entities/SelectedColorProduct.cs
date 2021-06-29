using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Domain.Entities
{
   public class SelectedColorProduct
    {
        public SelectedColorProduct()
        {

        }

        public string SelectedColorProductId { get; set; }
        public bool IsActive { get; set; }


        #region Relations

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string ProductColorId { get; set; }
        public ProductColor ProductColor { get; set; }

        #endregion
    }
}
