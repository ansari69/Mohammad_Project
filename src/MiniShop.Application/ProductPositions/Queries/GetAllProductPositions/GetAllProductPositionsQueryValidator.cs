using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductPositions.Queries.GetAllProductPositions
{
    public class GetAllProductPositionsQueryValidator
        : AbstractValidator<GetAllProductPositionsQuery>
    {
        public GetAllProductPositionsQueryValidator()
        {
            RuleFor(e => e.PageId)
              .GreaterThan(0);

            RuleFor(e => e.EachPerPage)
                .GreaterThan(0).LessThan(800);
        }
    }
}
