using FluentValidation;
using MiniShop.Application.ProductFeatures.Commands.DeleteProductFeature;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductColors.Commands.DeleteProductColor
{
   public class DeleteProductColorCommandValidator
        : AbstractValidator<DeleteProductColorCommand>
    {
        public DeleteProductColorCommandValidator()
        {
            RuleFor(e => e.ProductColorId)
            .NotNull().NotEmpty();
        }
    }
}
