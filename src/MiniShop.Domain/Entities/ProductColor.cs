using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Domain.Entities
{
   public class ProductColor
    {
        public ProductColor()
        {
            SelectedColorProducts = new List<SelectedColorProduct>();
        }


        public string ProductColorId { get; set; }
        public string PersianName { get; set; }
        public string LatinName { get; set; }
        public string ColorCode { get; set; }
        public bool IsActive { get; set; }


        #region Relations

        public IList<SelectedColorProduct> SelectedColorProducts { get; set; }

        #endregion


    }
}
