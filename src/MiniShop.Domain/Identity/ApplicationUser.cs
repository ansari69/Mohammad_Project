using Microsoft.AspNetCore.Identity;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Domain.Identity
{
   public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {

            CreatedProducts = new List<Product>();
            CreatedProductLikes = new List<ProductLike>();

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Rank { get; set; }
        public string MelliCode { get; set; }
        public DateTime CreateDate { get; set; }

        public bool IsManagmentConfirm { get; set; }
        public bool? IsActive { get; set; }



        #region Relations

        public IList<Product> CreatedProducts { get; set; }

        public IList<ProductLike> CreatedProductLikes { get; set; }

        #endregion

    }
}
