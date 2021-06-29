using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductFeatures.Queries.GetSingleProductFeature
{
   public class GetSingleProductFeatureQueryValidator
        : AbstractValidator<GetSingleProductFeatureQuery>
    {
        public GetSingleProductFeatureQueryValidator()
        {
            RuleFor(e => e.ProductFeatureId)
            .NotNull().NotEmpty();
        }
    }
}
