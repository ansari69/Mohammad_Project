using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetProductsForManager
{
    public class GetProductsForManagerQueryValidator
         : AbstractValidator<GetProductsForManagerQuery>
    {
        public GetProductsForManagerQueryValidator()
        {
            RuleFor(e => e.PageId)
            .GreaterThan(0);

            RuleFor(e => e.EachPerPage)
                .GreaterThan(0).LessThan(800);
        }
    }
}
