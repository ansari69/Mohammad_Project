using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductFeatures.Commands.UpsertProductFeature
{
    public class UpsertProductFeatureCommandValidator
        : AbstractValidator<UpsertProductFeatureCommand>
    {
        public UpsertProductFeatureCommandValidator()
        {
            //RuleFor(e => e.ProductFeatureId)
            //.NotNull().NotEmpty();

            RuleFor(e => e.FeatureName)
            .NotNull().NotEmpty();
        }
    }
}
