using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductFeatures.Commands.UpsertProductFeature
{
    public class UpsertProductFeatureCommand
         : IRequest<UpsertProductFeatureVM>
    {
        public string ProductFeatureId { get; set; }
        public string FeatureName { get; set; }
    }
}
