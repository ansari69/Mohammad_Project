using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Commands.InsertProduct
{
    public class InsertProductCommandValidator
        : AbstractValidator<InsertProductCommand>
    {
        public InsertProductCommandValidator()
        {
            RuleFor(e => e.ProductName)
            .NotNull().NotEmpty();

            RuleFor(e => e.ProductDisplayName)
            .NotNull().NotEmpty();

            RuleFor(e => e.Count)
            .GreaterThanOrEqualTo(0);

            RuleFor(e => e.Description)
           .NotNull().NotEmpty();

            RuleFor(e => e.Price)
            .GreaterThanOrEqualTo(0);

            RuleFor(e => e.ProductTypeId)
           .NotNull().NotEmpty();

            RuleFor(e => e.ProductPositionId)
           .NotNull().NotEmpty();

            RuleFor(e => e.ColorIds)
           .NotNull().NotEmpty();

            RuleFor(e => e.Features)
           .NotNull().NotEmpty();

            RuleFor(e => e.CreatorId)
            .NotNull().NotEmpty();

        }
    }
}
