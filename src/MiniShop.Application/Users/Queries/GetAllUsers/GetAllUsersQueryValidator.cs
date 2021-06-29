using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryValidator
         : AbstractValidator<GetAllUsersQuery>
    {
        public GetAllUsersQueryValidator()
        {
            RuleFor(e => e.PageId)
                .GreaterThan(0);

            RuleFor(e => e.EachPerPage)
                .GreaterThan(0).LessThan(800);
        }
    }
}
