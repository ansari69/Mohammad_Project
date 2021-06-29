using MediatR;
using MiniShop.Application.Common.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductTypes.Queries.GetSingleProductType
{
    public class GetSingleProductTypeQuery
        : IRequest<ProductTypeVM>
    {
        public string ProductTypeId { get; set; }

    }
}
