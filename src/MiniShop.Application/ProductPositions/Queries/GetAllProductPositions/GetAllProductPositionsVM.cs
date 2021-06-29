using MiniShop.Application.Common.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductPositions.Queries.GetAllProductPositions
{
   public class GetAllProductPositionsVM
    {
        public int PageId { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public IEnumerable<ProductPositionVM> ProductPositions { get; set; }
    }
}
