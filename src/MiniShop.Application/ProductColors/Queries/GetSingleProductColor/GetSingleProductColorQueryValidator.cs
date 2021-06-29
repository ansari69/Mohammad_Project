using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductColors.Queries.GetSingleProductColor
{
   public class GetSingleProductColorQueryValidator
        : AbstractValidator<GetSingleProductColorQuery>
    {
        public GetSingleProductColorQueryValidator()
        {
            RuleFor(e => e.ProductColorId)
            .NotNull().NotEmpty();
        }
    }
}
