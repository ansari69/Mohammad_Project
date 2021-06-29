using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MiniShop.Domain.Identity;
using MiniShop.Infrastructure.Security.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniShop.Infrastructure.Security.General
{
   public class SimpleRequirementHandler
        : AuthorizationHandler<SimpleRequirement>
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SimpleRequirementHandler(UserManager<ApplicationUser> userManger,
            RoleManager<ApplicationRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManger = userManger;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }



        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
          SimpleRequirement requirement)
        {
            // check user's authentication
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }


            // get user id
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Identifier");
            if (userIdClaim == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            // get user claims
            var finalClaims = AuthorizationHelper
                .GetUserClaimsByUserId(userIdClaim.Value, _userManger, _roleManager);


            ///**************************************
            //    ******   Grants Access If User Has These Conditions:
            //    ******   - Full Access
            //    ******   - Operator Access
            //    ******   - Simple Access
            //    ***************************************/
            if (finalClaims.Any(claim => claim == ApplicationClaimsStore.FullAccess.Name
                                         || claim == ApplicationClaimsStore.Operator.Name
                                         || claim == ApplicationClaimsStore.Simple.Name))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }


    }
}
