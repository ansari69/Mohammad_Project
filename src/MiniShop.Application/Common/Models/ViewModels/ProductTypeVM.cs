using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Models.ViewModels
{
   public class ProductTypeVM
    {
        public string ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
