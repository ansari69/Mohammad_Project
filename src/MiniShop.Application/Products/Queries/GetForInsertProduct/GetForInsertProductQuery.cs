using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetForInsertProduct
{
    public class GetForInsertProductQuery
        : IRequest<GetForInsertProductVM>
    {
    }
}
