using AutoMapper;
using MiniShop.Application.Common.Models.ViewModels;
using MiniShop.Application.ProductColors.Commands.UpsertProductColor;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Profiles
{
   public class ProductColorProfile : Profile
    {
        public ProductColorProfile()
        {
            CreateMap<UpsertProductColorCommand, ProductColor>()
              .ForAllMembers(opt => opt.Condition((src, dist, srcMember) => srcMember != null));

            CreateMap<ProductColor, ProductColorVM>();
        }
    }
}
