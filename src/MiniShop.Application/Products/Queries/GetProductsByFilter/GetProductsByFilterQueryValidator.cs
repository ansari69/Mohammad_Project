using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Queries.GetProductsByFilter
{
    public class GetProductsByFilterQueryValidator
        : AbstractValidator<GetProductsByFilterQuery>
    {
        public GetProductsByFilterQueryValidator()
        {
            RuleFor(e => e.PageId)
            .GreaterThan(0);

            RuleFor(e => e.EachPerPage)
                .GreaterThan(0).LessThan(800);
        }
    }
}
