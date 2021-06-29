using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductFeatures.Commands.DeleteProductFeature
{
   public class DeleteProductFeatureCommandValidator
        : AbstractValidator<DeleteProductFeatureCommand>
    {
        public DeleteProductFeatureCommandValidator()
        {
            RuleFor(e => e.ProductFeatureId)
            .NotNull().NotEmpty();
        }
    }
}
