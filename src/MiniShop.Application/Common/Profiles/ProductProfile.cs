using AutoMapper;
using MiniShop.Application.Common.Helpers;
using MiniShop.Application.Products.Queries.GetProductsByFilter;
using MiniShop.Application.Products.Queries.GetProductsForManager;
using MiniShop.Application.Products.Queries.GetSingleProduct;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Common.Profiles
{
   public class ProductProfile : Profile
    {
        public ProductProfile()
        {

            CreateMap<Product, ShowProductsByFilterVM>()
                .ForMember(d => d.ProductPositionName, a =>
                    a.MapFrom(s => s.ProductPosition.ProductPositionName))
                .ForMember(d => d.CreateDate, a => a.MapFrom(s => s.CreateDate.ToShamsiString()));


            CreateMap<Product, ShowProductsForManagerVM>()
                      .ForMember(d => d.ProductPositionName, a =>
                          a.MapFrom(s => s.ProductPosition.ProductPositionName))

                      .ForMember(d => d.ProductTypeName, a =>
                          a.MapFrom(s => s.ProductType.ProductTypeName))

                      .ForMember(d => d.CreatorFullName, a =>
                          a.MapFrom(s => s.Creator.FirstName + " " + s.Creator.LastName))
                      
                      .ForMember(d => d.CreateDate, a => a.MapFrom(s => s.CreateDate.ToShamsiString()));



            CreateMap<Product, GetSingleProductVM>()
                    .ForMember(d => d.Colors, a => a.Ignore())
                    .ForMember(d => d.Features, a => a.Ignore())
                      .ForMember(d => d.ProductPositionName, a =>
                          a.MapFrom(s => s.ProductPosition.ProductPositionName))

                      .ForMember(d => d.ProductTypeName, a =>
                          a.MapFrom(s => s.ProductType.ProductTypeName))

                      .ForMember(d => d.CreateDate, a => a.MapFrom(s => s.CreateDate.ToShamsiString()));


            CreateMap<SelectedColorProduct, ProductColorDTO>()
                    .ForMember(d => d.ProductColorId, a =>
                        a.MapFrom(s => s.ProductColor.ProductColorId))

                    .ForMember(d => d.LatinName, a =>
                        a.MapFrom(s => s.ProductColor.LatinName))

                    .ForMember(d => d.PersianName, a =>
                        a.MapFrom(s => s.ProductColor.PersianName))

                    .ForMember(d => d.ColorCode, a =>
                        a.MapFrom(s => s.ProductColor.ColorCode))              
                    ;

            CreateMap<ProductFeaturesValue, ProductFeaturesValueDTO>()
                   .ForMember(d => d.FeaturesValue, a =>
                       a.MapFrom(s => s.FeaturesValue))

                   .ForMember(d => d.FeatureName, a =>
                       a.MapFrom(s => s.ProductFeature.FeatureName))
                   ;


        }

    }
}
