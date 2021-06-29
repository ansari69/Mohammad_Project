using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductTypes.Queries.GetAllProductTypes
{
   public class GetAllProductTypesQueryValidator
        : AbstractValidator<GetAllProductTypesQuery>
    {
        public GetAllProductTypesQueryValidator()
        {
            RuleFor(e => e.PageId)
              .GreaterThan(0);

            RuleFor(e => e.EachPerPage)
                .GreaterThan(0).LessThan(800);
        }
    }
}
