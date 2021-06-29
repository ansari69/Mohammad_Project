using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductTypes.Commands.DeleteProductType
{
   public class DeleteProductTypeValidator
        : AbstractValidator<DeleteProductTypeCommand>
    {
        public DeleteProductTypeValidator()
        {
            RuleFor(e => e.ProductTypeId)
            .NotNull().NotEmpty();
        }
    }
}
