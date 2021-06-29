using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetProductsByFilter
{
    public class GetProductsByFilterVM
    {
        public int PageId { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public IEnumerable<ShowProductsByFilterVM> Products { get; set; }
    }
}
