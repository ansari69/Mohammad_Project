using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Users.Commands.InsertUser
{
    public class InsertUserCommandValidator     
        : AbstractValidator<InsertUserCommand>
    {
        public InsertUserCommandValidator()
        {
            RuleFor(e => e.UserName)
                .NotNull().NotEmpty()
                .MaximumLength(50);

            RuleFor(e => e.Password)
                .NotNull().NotEmpty()
                .MaximumLength(32);

            RuleFor(c => c.ConfirmPassword)
              .NotNull().NotEmpty()
              .Equal(c => c.Password);

            RuleFor(e => e.FirstName)
                .NotNull().NotEmpty();

            RuleFor(e => e.LastName)
                .NotNull().NotEmpty();
        }
    }
}
