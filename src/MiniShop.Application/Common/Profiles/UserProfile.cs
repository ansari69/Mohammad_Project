using AutoMapper;
using MiniShop.Application.Common.Helpers;
using MiniShop.Application.Users.Commands.InsertUser;
using MiniShop.Application.Users.Queries.GetAllUsers;
using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Profiles
{
   public class UserProfile : Profile
    {
        public UserProfile()
        {
            //Get by filter
            CreateMap<ApplicationUser, ShowUsersVM>()
                .ForMember(d => d.UserId, a => a.MapFrom(s => s.Id))
                .ForMember(d => d.RegisterDate, a => a.MapFrom(s => s.CreateDate.ToShamsiString()));


            //Upsert
            CreateMap<InsertUserCommand, ApplicationUser>()
                .ForAllMembers(opt => opt.Condition((src, dist, srcMember) => srcMember != null));

        }
    }
}
