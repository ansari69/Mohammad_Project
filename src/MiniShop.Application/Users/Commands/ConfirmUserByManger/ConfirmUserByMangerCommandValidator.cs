using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Users.Commands.ConfirmUserByManger
{
    public class ConfirmUserByMangerCommandValidator
         : AbstractValidator<ConfirmUserByMangerCommand>
    {
        public ConfirmUserByMangerCommandValidator()
        {

        }
    }
}
