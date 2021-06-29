using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetSingleProduct
{
   public class GetSingleProductQuery
        : IRequest<GetSingleProductVM>
    {
        public string ProductId { get; set; }

    }
}
