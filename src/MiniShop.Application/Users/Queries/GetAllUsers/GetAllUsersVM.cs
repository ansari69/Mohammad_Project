using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersVM
    {
        public int TotalPages { get; set; }
        public int TotalResult { get; set; }
        public int PageId { get; set; }
        public IEnumerable<ShowUsersVM> Users { get; set; }
    }
}
