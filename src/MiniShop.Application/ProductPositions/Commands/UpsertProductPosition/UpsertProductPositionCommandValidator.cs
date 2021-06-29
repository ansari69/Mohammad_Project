using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductPositions.Commands.UpsertProductPosition
{
    public class UpsertProductPositionCommandValidator
        : AbstractValidator<UpsertProductPositionCommand>
    {
        public UpsertProductPositionCommandValidator()
        {
            //RuleFor(e => e.ProductPositionId)
            //.NotNull().NotEmpty();

            RuleFor(e => e.ProductPositionName)
            .NotNull().NotEmpty();
        }
    }
}
