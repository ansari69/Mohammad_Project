using MiniShop.Application.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Exceptions
{
   public class OperationFailedException
        : Exception
    {
        public OperationFailedException()
            : base(ErrorMessages.OperationFailed)
        {
            Source = "Application";
        }
    }
}
