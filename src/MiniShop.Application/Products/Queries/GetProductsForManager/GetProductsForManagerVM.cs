using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetProductsForManager
{
    public class GetProductsForManagerVM
    {
        public int PageId { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public IEnumerable<ShowProductsForManagerVM> Products { get; set; }
    }
}
