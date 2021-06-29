using MiniShop.Application.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Exceptions
{
   public  class EntryValidationException : Exception
    {
        public EntryValidationException()
            : base(ErrorMessages.EntryNotValid)
        {
            Source = "Application";
        }
    }
}
