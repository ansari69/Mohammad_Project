using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetImageProduct
{
    public class GetImageProductQuery
        : IRequest<string>
    {
        public string ProductId { get; set; }

    }
}
