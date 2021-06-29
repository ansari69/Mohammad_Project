using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Users.Commands.UpsertUserRoles
{
   public class UpsertUserRolesCommandHandler
        : IRequestHandler<UpsertUserRolesCommand, UpsertUserRolesVM>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UpsertUserRolesCommandHandler(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public async Task<UpsertUserRolesVM> Handle(UpsertUserRolesCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
                throw new EntryValidationException();

            foreach (var incoming in request.UserRoles.Distinct())
            {
                var isRoleExists = await _roleManager.RoleExistsAsync(incoming);
                if (!isRoleExists)
                    throw new EntryValidationException();
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                var deleteResult = await _userManager.RemoveFromRolesAsync(user, roles.ToList());
                if (!deleteResult.Succeeded)
                    throw new OperationFailedException();
            }

            var addResult = await _userManager.AddToRolesAsync(user, request.UserRoles.Distinct().ToList());
            if (!addResult.Succeeded)
                throw new OperationFailedException();

            return new UpsertUserRolesVM()
            {
                UserId = user.Id,
                UserRoles = request.UserRoles
            };
        }
    }
}
