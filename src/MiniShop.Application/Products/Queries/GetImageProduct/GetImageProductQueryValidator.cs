using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetImageProduct
{
   public class GetImageProductQueryValidator
        : AbstractValidator<GetImageProductQuery>
    {
        public GetImageProductQueryValidator()
        {
            RuleFor(e => e.ProductId)
            .NotNull().NotEmpty();
        }
    }
}
