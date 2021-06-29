using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductColors.Commands.UpsertProductColor
{
    public class UpsertProductColorCommandValidator
        : AbstractValidator<UpsertProductColorCommand>
    {
        public UpsertProductColorCommandValidator()
        {
 
            RuleFor(e => e.PersianName)
            .NotNull().NotEmpty();

            RuleFor(e => e.LatinName)
            .NotNull().NotEmpty();

            RuleFor(e => e.ColorCode)
            .NotNull().NotEmpty();
        }
    }
}
