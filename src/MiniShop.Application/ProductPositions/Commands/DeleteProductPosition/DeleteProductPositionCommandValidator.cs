using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductPositions.Commands.DeleteProductPosition
{
   public class DeleteProductPositionCommandValidator
        : AbstractValidator<DeleteProductPositionCommand>
    {
        public DeleteProductPositionCommandValidator()
        {
            RuleFor(e => e.ProductPositionId)
            .NotNull().NotEmpty();
        }
    }
}
