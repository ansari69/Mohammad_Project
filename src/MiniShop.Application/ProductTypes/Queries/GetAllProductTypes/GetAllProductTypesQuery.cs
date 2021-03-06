using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductTypes.Queries.GetAllProductTypes
{
    public class GetAllProductTypesQuery : IRequest<GetAllProductTypesVM>
    {
        public int PageId { get; set; } = 1;
        public int EachPerPage { get; set; } = 12;
        public string SearchValue { get; set; } = "";

      //  public string SortBy { get; set; } = "";
      //  public bool SortByDescending { get; set; } = false;

    }
}
