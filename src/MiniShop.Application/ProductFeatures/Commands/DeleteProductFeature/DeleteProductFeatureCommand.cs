using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductFeatures.Commands.DeleteProductFeature
{
   public class DeleteProductFeatureCommand
        : IRequest<bool>
    {
        public string ProductFeatureId { get; set; }

    }
}
