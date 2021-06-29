using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Users.Commands.ConfirmUserByManger
{
    public class ConfirmUserByMangerCommandHandler
        : IRequestHandler<ConfirmUserByMangerCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ConfirmUserByMangerCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(ConfirmUserByMangerCommand request, CancellationToken cancellationToken)
        {

            // Get user
            var user = await _userManager.FindByIdAsync(request.UserId);

            // Validate user
            if (user == null || !user.IsActive.Value || user.IsManagmentConfirm)
                throw new EntryValidationException();

            user.IsManagmentConfirm = true;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new EntryValidationException();

            return true;


        }
    }
}
