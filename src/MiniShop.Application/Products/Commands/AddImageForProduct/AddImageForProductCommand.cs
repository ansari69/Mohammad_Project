using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Commands.AddImageForProduct
{
   public class AddImageForProductCommand
        : IRequest<bool>
    {
        public string ProductId { get; set; }
        public IFormFile Files { get; set; }

    }
}
