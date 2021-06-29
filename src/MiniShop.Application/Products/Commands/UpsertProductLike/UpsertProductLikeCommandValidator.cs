using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Commands.UpsertProductLike
{
   public class UpsertProductLikeCommandValidator
        : AbstractValidator<UpsertProductLikeCommand>
    {
        public UpsertProductLikeCommandValidator()
        {
            RuleFor(e => e.ProductId)
            .NotNull().NotEmpty();

            RuleFor(e => e.CreatorId)
            .NotNull().NotEmpty();
        }
    }
}
