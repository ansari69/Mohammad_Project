using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductPositions.Queries.GetSingleProductPosition
{
   public class GetSingleProductPositionQueryValidator
        : AbstractValidator<GetSingleProductPositionQuery>
    {
        public GetSingleProductPositionQueryValidator()
        {
            RuleFor(e => e.ProductPositionId)
            .NotNull().NotEmpty();
        }
    }
}
