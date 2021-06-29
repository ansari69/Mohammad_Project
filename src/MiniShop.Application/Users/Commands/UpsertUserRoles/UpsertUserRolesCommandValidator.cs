using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Users.Commands.UpsertUserRoles
{
  public  class UpsertUserRolesCommandValidator
        : AbstractValidator<UpsertUserRolesCommand>
    {
        public UpsertUserRolesCommandValidator()
        {
            RuleFor(e => e.UserId)
                .NotNull().NotEmpty()
                .MaximumLength(200);

            RuleFor(e => e.UserRoles)
                .NotNull().NotEmpty();
        }
    }
}
