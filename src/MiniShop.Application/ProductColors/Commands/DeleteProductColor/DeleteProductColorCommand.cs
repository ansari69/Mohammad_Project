using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductColors.Commands.DeleteProductColor
{
    public class DeleteProductColorCommand
        : IRequest<bool>
    {
        public string ProductColorId { get; set; }
    }
}
