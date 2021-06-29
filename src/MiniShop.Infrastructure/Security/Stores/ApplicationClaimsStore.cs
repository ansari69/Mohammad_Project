using MiniShop.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Infrastructure.Security.Stores
{
   public static class ApplicationClaimsStore
    {

        public static readonly ApplicationClaimModel FullAccess =
            new ApplicationClaimModel("FullAccess", "دسترسی کامل", true)
            {
                Description = "دسترسی کامل به تمام تنظیمات سایت"
            };

        public static readonly ApplicationClaimModel Operator =
            new ApplicationClaimModel("Operator", "دسترسی محدود")
            {
                Description = "دسترسی به ثبت محصولات"
            };

        public static readonly ApplicationClaimModel Simple =
           new ApplicationClaimModel("Simple", "کاربر عادی")
           {
               Description = "کاربر عادی"
           };


        //Get All Claims
        public static List<ApplicationClaimModel> AllClaims
        {
            get
            {
                var result = new List<ApplicationClaimModel>();
                var properties = typeof(ApplicationClaimsStore).GetFields();

                foreach (var prop in properties)
                {
                    var myClaim = prop.GetValue(null) as ApplicationClaimModel;
                    result.Add(myClaim);
                }

                return result;
            }
        }

    }
}
