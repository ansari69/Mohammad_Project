using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetSingleProduct
{
    public class GetSingleProductQueryValidator
         : AbstractValidator<GetSingleProductQuery>
    {
        public GetSingleProductQueryValidator()
        {
            RuleFor(e => e.ProductId)
            .NotNull().NotEmpty();
        }
    }
}
