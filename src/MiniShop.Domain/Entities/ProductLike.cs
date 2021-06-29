using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Domain.Entities
{
   public class ProductLike
    {
        public ProductLike()
        {

        }


        public string ProductLikeId { get; set; }
        public bool IsLike { get; set; }


        #region Relations

        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

        #endregion



    }
}
