using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductTypes.Commands.DeleteProductType
{
   public class DeleteProductTypeCommand
        : IRequest<bool>
    {
        public string ProductTypeId { get; set; }
    }
}
