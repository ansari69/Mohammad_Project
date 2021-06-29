using AutoMapper;
using MiniShop.Application.Common.Models.ViewModels;
using MiniShop.Application.ProductColors.Commands.UpsertProductColor;
using MiniShop.Application.ProductFeatures.Commands.UpsertProductFeature;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Profiles
{
   public class ProductFeatureProfile : Profile
    {
        public ProductFeatureProfile()
        {
            CreateMap<UpsertProductFeatureCommand, ProductFeature>()
               .ForAllMembers(opt => opt.Condition((src, dist, srcMember) => srcMember != null));

           CreateMap<ProductFeature, ProductFeatureVM>();
        }
    }
}
