using MediatR;
using MiniShop.Application.Common.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductColors.Queries.GetSingleProductColor
{
    public class GetSingleProductColorQuery
        : IRequest<ProductColorVM>
    {
        public string ProductColorId { get; set; }

    }
}
