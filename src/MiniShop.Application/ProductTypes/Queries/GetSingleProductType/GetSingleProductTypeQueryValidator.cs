using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductTypes.Queries.GetSingleProductType
{
   public class GetSingleProductTypeQueryValidator
        : AbstractValidator<GetSingleProductTypeQuery>
    {
        public GetSingleProductTypeQueryValidator()
        {
            RuleFor(e => e.ProductTypeId)
            .NotNull().NotEmpty();
        }
    }
}
