using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductFeatures.Queries.GetAllProductFeatures
{
    public class GetAllProductFeaturesQuery
        : IRequest<GetAllProductFeaturesVM>
    {
        public int PageId { get; set; } = 1;
        public int EachPerPage { get; set; } = 12;
        public string SearchValue { get; set; } = "";
    }
}
