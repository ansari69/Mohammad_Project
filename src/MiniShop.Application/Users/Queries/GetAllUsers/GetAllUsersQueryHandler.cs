using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Helpers.ListFilterExtentions;
using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler
        : IRequestHandler<GetAllUsersQuery, GetAllUsersVM>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper _mapper;


        public GetAllUsersQueryHandler(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<GetAllUsersVM> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {

            //getting users as iqueryable
            var users = _userManager.Users.Where(u => u.IsActive == request.GetActives);

            //Filter username or email
            if (!String.IsNullOrEmpty(request.SearchValue))
                users = users.Where(u => u.UserName.Contains(request.SearchValue)
                                        || u.Email.Contains(request.SearchValue)
                                        || (u.FirstName + " " + u.LastName).Contains(request.SearchValue)
                                        || u.MelliCode.StartsWith(request.SearchValue));

            //Filter sort by
            if (!String.IsNullOrEmpty(request.SortBy))
            {
                try
                {
                    users = users.FilterOrderBy(request.SortBy, request.SortByDescending);
                }
                catch
                {
                    throw new Exception();
                }
            }

            var totalPage = users.TotalPagesCount(request.EachPerPage);
            var totalResult = users.Count();

            //Fail if no result found
            if (totalPage == 0)
                return new GetAllUsersVM()
                {
                    TotalPages = totalPage,
                    PageId = request.PageId,
                    Users = new List<ShowUsersVM>()
                };

            //if pageId is larger than total page return last page (cuz that make no sense)
            if (request.PageId > totalPage) request.PageId = totalPage;


            //paging and cast to view model
            var usersResult = await users.ToPaginatedQuery(request.PageId, request.EachPerPage)
                .ToListAsync();


            var resultList =
                _mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ShowUsersVM>>(usersResult);

            var result = new GetAllUsersVM()
            {
                TotalPages = totalPage,
                TotalResult = totalResult,
                PageId = request.PageId,
                Users = resultList
            };

            return result;

        }
    }
}
