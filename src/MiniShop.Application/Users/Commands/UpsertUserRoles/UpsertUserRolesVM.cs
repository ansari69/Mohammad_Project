using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Users.Commands.UpsertUserRoles
{
   public class UpsertUserRolesVM
    {
        public string UserId { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}
