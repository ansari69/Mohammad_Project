using MediatR;
using MiniShop.Application.Common.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductFeatures.Queries.GetSingleProductFeature
{
   public class GetSingleProductFeatureQuery
         : IRequest<ProductFeatureVM>
    {
        public string ProductFeatureId { get; set; }

    }
}
