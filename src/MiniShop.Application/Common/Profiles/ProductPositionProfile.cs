using AutoMapper;
using MiniShop.Application.Common.Models.ViewModels;
using MiniShop.Application.ProductPositions.Commands.UpsertProductPosition;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Profiles
{
   public class ProductPositionProfile : Profile
    {
        public ProductPositionProfile()
        {
            CreateMap<UpsertProductPositionCommand, ProductPosition>()
               .ForAllMembers(opt => opt.Condition((src, dist, srcMember) => srcMember != null));

              CreateMap<ProductPosition, ProductPositionVM>();
        }
    }
}
