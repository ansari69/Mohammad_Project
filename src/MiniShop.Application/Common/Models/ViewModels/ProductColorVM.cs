using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Models.ViewModels
{
   public class ProductColorVM
    {
        public string ProductColorId { get; set; }
        public string PersianName { get; set; }
        public string LatinName { get; set; }
        public string ColorCode { get; set; }
        public bool IsActive { get; set; }
    }
}
