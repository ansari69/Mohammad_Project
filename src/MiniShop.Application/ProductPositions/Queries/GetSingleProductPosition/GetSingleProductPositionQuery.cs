using MediatR;
using MiniShop.Application.Common.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductPositions.Queries.GetSingleProductPosition
{
    public class GetSingleProductPositionQuery
        : IRequest<ProductPositionVM>
    {
        public string ProductPositionId { get; set; }
    }
}
