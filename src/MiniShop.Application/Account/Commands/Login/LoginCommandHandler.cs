using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Helpers;
using MiniShop.Application.Common.Models;
using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Account.Commands.Login
{
    public class LoginCommandHandler
        : IRequestHandler<LoginCommand, LoginVM>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly AppSettings _appSettings;
        private readonly IMediator _mediator;

        public LoginCommandHandler(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IOptions<AppSettings> appSettings,
            IMediator mediator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSettings.Value;
            _mediator = mediator;
        }

        public async Task<LoginVM> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            //var loginResult = await _signInManager.PasswordSignInAsync(request.Username,
            //    request.Password, true, false);

            //if (loginResult.Succeeded)
            //{
                //get user
                var user = await _userManager.FindByNameAsync(request.Username);

              if (user == null || !user.IsActive.Value || !user.IsManagmentConfirm)
                throw new EntryValidationException();

            //getting user claims
            var claims = (await _userManager.GetClaimsAsync(user)).Select(c => c.Type).ToList();

                var roles = (await _userManager.GetRolesAsync(user));

                // get role's claims
                foreach (var role in roles)
                {
                    var appRole = await _roleManager.FindByNameAsync(role);
                    var roleClaims = (await _roleManager.GetClaimsAsync(appRole)).Select(c => c.Type);
                    claims.AddRange(roleClaims);
                }

                claims = claims.Distinct().ToList();


                var fullName = $"{user.FirstName} {user.LastName}";
                var rank = (user.Rank == null) ? "کاربر سیستم" : user.Rank;

                var token = await Generator.GenerateTokenAsync(_appSettings, user.Id,
                    user.UserName, fullName, claims, roles, rank);

                if (String.IsNullOrEmpty(token))
                throw new LoginFailedException();





               return new LoginVM()
                {
                    Token = token
                };
            //  }

          //  throw new LoginFailedException();
        }
    }
}
