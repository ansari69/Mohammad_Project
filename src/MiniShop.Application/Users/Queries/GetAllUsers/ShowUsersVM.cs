using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Users.Queries.GetAllUsers
{
   public class ShowUsersVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Rank { get; set; }
        public string RegisterDate { get; set; }
    }
}
