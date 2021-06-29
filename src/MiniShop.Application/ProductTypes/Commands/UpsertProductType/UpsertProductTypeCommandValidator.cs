using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductTypes.Commands.UpsertProductType
{
   public class UpsertProductTypeCommandValidator :
        AbstractValidator<UpsertProductTypeCommand>
    {
        public UpsertProductTypeCommandValidator()
        {
          //RuleFor(e => e.ProductTypeId)
          //.NotNull().NotEmpty();

          RuleFor(e => e.ProductTypeName)
          .NotNull().NotEmpty();

          RuleFor(e => e.Description)
          .NotNull().NotEmpty();
        }
    }
}
