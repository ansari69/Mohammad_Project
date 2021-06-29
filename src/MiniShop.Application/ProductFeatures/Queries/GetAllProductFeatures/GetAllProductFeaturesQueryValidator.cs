using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductFeatures.Queries.GetAllProductFeatures
{
    public class GetAllProductFeaturesQueryValidator
        : AbstractValidator<GetAllProductFeaturesQuery>
    {
        public GetAllProductFeaturesQueryValidator()
        {
            RuleFor(e => e.PageId)
              .GreaterThan(0);

            RuleFor(e => e.EachPerPage)
                .GreaterThan(0).LessThan(800);
        }
    }
}
