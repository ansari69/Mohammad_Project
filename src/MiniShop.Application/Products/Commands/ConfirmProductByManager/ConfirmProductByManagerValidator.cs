using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Commands.ConfirmProductByManager
{
   public class ConfirmProductByManagerValidator
        : AbstractValidator<ConfirmProductByManagerCommand>
    {
        public ConfirmProductByManagerValidator()
        {
            RuleFor(e => e.ProductId)
             .NotNull().NotEmpty();
        }
    }
}
