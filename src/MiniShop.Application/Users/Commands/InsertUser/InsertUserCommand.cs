using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Users.Commands.InsertUser
{
    public class InsertUserCommand
         : IRequest<InsertUserCommandVM>
    {
      //  public string UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MelliCode { get; set; }
        public string Rank { get; set; }
    }
}
