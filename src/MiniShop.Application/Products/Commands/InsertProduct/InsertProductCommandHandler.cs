using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Domain.Entities;
using MiniShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Products.Commands.InsertProduct
{
    public class InsertProductCommandHandler
        : IRequestHandler<InsertProductCommand, InsertProductVM>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public InsertProductCommandHandler(IAppDbContext context, IMapper mapper,
            UserManager<ApplicationUser> userManger,
            RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _mapper = mapper;
            _userManger = userManger;
            _roleManager = roleManager;
        }

        public async Task<InsertProductVM> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {

            #region Validate

            // Validate ProductType
            var isProductType = await _context.ProductTypes.AsNoTracking()
                .AnyAsync(e => e.IsActive && e.ProductTypeId == request.ProductTypeId);

            // ValidateProduct Positions
            var isProductStatus = await _context.ProductPositions.AsNoTracking()
                .AnyAsync(e => e.IsActive && e.ProductPositionId == request.ProductPositionId);

            if (!isProductStatus || !isProductType)
                throw new EntryValidationException();


            // Validate color
            foreach (var colorId in request.ColorIds.Distinct())
            {              
                var isColor = await _context.ProductColors.AsNoTracking()
                    .AnyAsync(e => e.IsActive && e.ProductColorId == colorId);

                if (!isColor)
                    throw new EntryValidationException();
            }


            // Validate Product Feature
            foreach (var featureModel in request.Features.Distinct())
            {

                if (featureModel.ProductFeatureId == null || featureModel.FeaturesValue == null)
                    throw new EntryValidationException();

                var isFeature = await _context.ProductFeatures.AsNoTracking()
                    .AnyAsync(e => e.IsActive && 
                             e.ProductFeatureId == featureModel.ProductFeatureId);

                if (!isFeature)
                    throw new EntryValidationException();
            }

            #endregion

            Product product = new Product();

            product.ProductId = Guid.NewGuid().ToString();
            product.ProductName = request.ProductName;
            product.ProductDisplayName = request.ProductDisplayName;
            product.Description = request.Description;
            product.Count = request.Count;
            product.Price = request.Price;
            product.ProductTypeId = request.ProductTypeId;
            product.ProductPositionId = request.ProductPositionId;
            product.IsActive = true;
            product.CreateDate = DateTime.Now;
            product.CreatorId = request.CreatorId;

            _context.Products.Add(product);


            #region User Cheack

            // get user's claims
            var userClaims = new List<Claim>();
            var user = _userManger.FindByIdAsync(request.CreatorId).Result;
            var claims = _userManger.GetClaimsAsync(user).Result;
            var roles = _userManger.GetRolesAsync(user).Result;

            foreach (var role in roles)
            {
                var identityRole = _roleManager.FindByNameAsync(role).Result;
                userClaims.AddRange(_roleManager.GetClaimsAsync(identityRole).Result);
            }

            userClaims.AddRange(claims);

            var finalClaims = userClaims.Select(c => c.Type).Distinct().ToList();

            // Check manager or operator
            if (finalClaims.Any(claim => claim == "FullAccess"))
                product.IsConfirm = true;


            #endregion


            #region Insert color for product

                // Insert color for Product
            foreach (var colorId in request.ColorIds.Distinct())
            {
                var colorProduct = new SelectedColorProduct();

                colorProduct.SelectedColorProductId = Guid.NewGuid().ToString();
                colorProduct.IsActive = true;
                colorProduct.ProductId = product.ProductId;
                colorProduct.ProductColorId = colorId;

                _context.SelectedColorProducts.Add(colorProduct);
            }

            #endregion


            #region Insert feature for product 

            // Insert Product Feature
            foreach (var featureModel in request.Features.Distinct())
            {

                var featuresValue = new ProductFeaturesValue();

                featuresValue.ProductFeaturesValueId = Guid.NewGuid().ToString();

                featuresValue.IsActive = true;

                featuresValue.ProductId = product.ProductId;

                featuresValue.ProductFeatureId = featureModel.ProductFeatureId;
                featuresValue.FeaturesValue = featureModel.FeaturesValue;

                _context.ProductFeaturesValues.Add(featuresValue);

            }

            #endregion



            #region File

            //var aaa = "";

            //long size = request.files.Sum(f => f.Length);

            //var filePaths = new List<string>();
            //foreach (var formFile in request.files)
            //{
            //    if (formFile.Length > 0)
            //    {

            //        string mimeType = "application/unknown";
            //        string ext = System.IO.Path.GetExtension(formFile.FileName).ToLower();
            //        Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            //        if (regKey != null && regKey.GetValue("Content Type") != null)
            //            mimeType = regKey.GetValue("Content Type").ToString();


            //        aaa = aaa + " // " + formFile.FileName + " -- " + mimeType;

            //        // formFile.ContentType.

            //        var fileName = Guid.NewGuid().ToString();

            //        // full path to file in temp location  "TestTest"
            //        var filePath = Path.Combine("Products", fileName);
            //        filePaths.Add(filePath);
            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //            await formFile.CopyToAsync(stream);
            //        }

            //        product.ImageName = fileName;
            //    }
            //}



            #endregion


            // Save DataBase
            await _context.SaveChangesAsync();


            return new InsertProductVM
            {
                ProductId = product.ProductId
            };

        }
    }
}
