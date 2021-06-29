using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Account.Commands.Login
{
    public class LoginCommand :  IRequest<LoginVM>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
