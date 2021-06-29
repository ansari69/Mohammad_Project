using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Commands.AddImageForProduct
{
    public class AddImageForProductCommandValidator
        : AbstractValidator<AddImageForProductCommand>
    {
        public AddImageForProductCommandValidator()
        {
            RuleFor(e => e.ProductId)
             .NotNull().NotEmpty();
        }
    }
}
