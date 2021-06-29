using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Commands.ConfirmProductByManager
{
    public class ConfirmProductByManagerCommand
        : IRequest<bool>
    {
        public string ProductId { get; set; }
    }
}
