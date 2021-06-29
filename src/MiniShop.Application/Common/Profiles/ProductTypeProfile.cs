using AutoMapper;
using MiniShop.Application.Common.Models.ViewModels;
using MiniShop.Application.ProductTypes.Commands.UpsertProductType;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Profiles
{
  public class ProductTypeProfile : Profile
    {
        public ProductTypeProfile()
        {
            CreateMap<UpsertProductTypeCommand, ProductType>()
               .ForAllMembers(opt => opt.Condition((src, dist, srcMember) => srcMember != null));

            CreateMap<ProductType, ProductTypeVM>();

        }
    }
}
