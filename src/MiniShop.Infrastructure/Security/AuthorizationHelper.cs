using Microsoft.AspNetCore.Identity;
using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MiniShop.Infrastructure.Security
{
   public class AuthorizationHelper
    {

        public static List<string> GetUserClaimsByUserId(string userId,
          UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            // get user's claims
            var userClaims = new List<Claim>();
            var user = userManager.FindByIdAsync(userId).Result;
            var claims = userManager.GetClaimsAsync(user).Result;
            var roles = userManager.GetRolesAsync(user).Result;

            foreach (var role in roles)
            {
                var identityRole = roleManager.FindByNameAsync(role).Result;
                userClaims.AddRange(roleManager.GetClaimsAsync(identityRole).Result);
            }

            userClaims.AddRange(claims);
            return userClaims.Select(c => c.Type).Distinct().ToList();
        }


        public static List<string> GetRoleClaimsByRoleId(string roleId, RoleManager<ApplicationRole> roleManager)
        {
            var role = roleManager.FindByIdAsync(roleId).Result;
            var claims = roleManager.GetClaimsAsync(role).Result;

            return claims.Select(c => c.Type).Distinct().ToList();
        }

    }
}
