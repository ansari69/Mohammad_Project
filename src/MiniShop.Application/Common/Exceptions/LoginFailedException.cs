using MiniShop.Application.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Exceptions
{
   public class LoginFailedException : Exception
    {

        public LoginFailedException()
            : base(ErrorMessages.LoginFailed)
        {
            Source = "Application";
        }

    }
}
