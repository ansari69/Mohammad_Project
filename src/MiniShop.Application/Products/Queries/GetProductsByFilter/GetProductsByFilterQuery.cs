using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetProductsByFilter
{
    public class GetProductsByFilterQuery
        : IRequest<GetProductsByFilterVM>
    {
        public int PageId { get; set; } = 1;
        public int EachPerPage { get; set; } = 12;
        public string SearchValue { get; set; } = "";
        public string SortBy { get; set; } = "";
        public bool SortByDescending { get; set; } = false;

        public string ProductPositionId { get; set; }
        public string ProductTypeId { get; set; }




    }
}
