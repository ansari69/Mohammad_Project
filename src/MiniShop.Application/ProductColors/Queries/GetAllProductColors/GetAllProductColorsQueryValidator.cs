using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductColors.Queries.GetAllProductColors
{
    public class GetAllProductColorsQueryValidator
        : AbstractValidator<GetAllProductColorsQuery>
    {
        public GetAllProductColorsQueryValidator()
        {
            RuleFor(e => e.PageId)
              .GreaterThan(0);

            RuleFor(e => e.EachPerPage)
                .GreaterThan(0).LessThan(800);
        }
    }
}
