using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Users.Commands.ConfirmUserByManger
{
   public class ConfirmUserByMangerCommand
         : IRequest<bool>
    {
        public string UserId { get; set; }

    }
}
