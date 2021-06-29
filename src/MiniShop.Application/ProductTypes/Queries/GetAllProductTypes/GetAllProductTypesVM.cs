using MiniShop.Application.Common.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductTypes.Queries.GetAllProductTypes
{
   public class GetAllProductTypesVM
    {
        public int PageId { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }

        public IEnumerable<ProductTypeVM> ProductTypes { get; set; }
    }
}
