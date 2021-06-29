using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetProductsByFilter
{
   public class ShowProductsByFilterVM
    {

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDisplayName { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }

        public string ProductPositionId { get; set; }
        public string ProductPositionName { get; set; }


    }
}
