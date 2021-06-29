using MiniShop.Application.Common.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductColors.Queries.GetAllProductColors
{
   public class GetAllProductColorsVM
    {
        public int PageId { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public IEnumerable<ProductColorVM> ProductColors { get; set; }
    }
}
