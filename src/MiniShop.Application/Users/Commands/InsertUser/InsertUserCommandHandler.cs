using AutoMapper;
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

namespace MiniShop.Application.Users.Commands.InsertUser
{
    public class InsertUserCommandHandler
        : IRequestHandler<InsertUserCommand, InsertUserCommandVM>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;


        public InsertUserCommandHandler(UserManager<ApplicationUser> userManager,
             RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<InsertUserCommandVM> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {

              var user = new ApplicationUser();

           
                if (request.UserName == null || request.Password == null)
                throw new EntryValidationException();

                user.UserName = request.UserName;
                user.CreateDate = DateTime.Now;
                user.IsActive = true;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
               user.IsManagmentConfirm = false;

               user.Email = request.Email;
               user.PhoneNumber = request.PhoneNumber;

            var createResult = await _userManager.CreateAsync(user, request.Password);

                if (!createResult.Succeeded)
                   throw new EntryValidationException();


            user = _mapper.Map<InsertUserCommand, ApplicationUser>(request, user);


            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                throw new EntryValidationException();

            var role = await _roleManager.FindByNameAsync("عادی");

            if (role != null)
            {
                var addResult = await _userManager.AddToRoleAsync(user, role.Name);
                if (!addResult.Succeeded)
                    throw new EntryValidationException();

            }
            else
                throw new EntryValidationException();


            return new InsertUserCommandVM() { UserId = user.Id };


        }
    }
}
