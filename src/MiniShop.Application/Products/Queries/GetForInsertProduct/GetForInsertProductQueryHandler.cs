using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Application.Common.Models.ViewModels;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Products.Queries.GetForInsertProduct
{
    public class GetForInsertProductQueryHandler
        : IRequestHandler<GetForInsertProductQuery, GetForInsertProductVM>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetForInsertProductQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetForInsertProductVM> Handle(GetForInsertProductQuery request, CancellationToken cancellationToken)
        {

            return await Task.Run(() =>
            {

             var result = new GetForInsertProductVM();


             #region Get colors

            var productColors = _context.ProductColors.AsNoTracking()
                .Where(b => b.IsActive == true);

            if(productColors.Any())
             result.ProductColors = _mapper.Map<IEnumerable<ProductColor>, IEnumerable<ProductColorVM>>
                     (productColors);

            #endregion


            #region Get Product Types

            var productTypes = _context.ProductTypes.AsNoTracking()
                .Where(b => b.IsActive == true);

            if (productTypes.Any())
                result.ProductTypes = _mapper.Map<IEnumerable<ProductType>, IEnumerable<ProductTypeVM>>
                     (productTypes);

            #endregion


            #region Get Product Positions

            var productPositions = _context.ProductPositions.AsNoTracking()
                .Where(b => b.IsActive == true);

            if (productPositions.Any())
                result.ProductPositions = _mapper.Map<IEnumerable<ProductPosition>, IEnumerable<ProductPositionVM>>
                     (productPositions);

            #endregion


            #region Get Product Features

            var productFeatures = _context.ProductFeatures.AsNoTracking()
                .Where(b => b.IsActive == true);

            if (productFeatures.Any())
                result.ProductFeatures = _mapper.Map<IEnumerable<ProductFeature>, IEnumerable<ProductFeatureVM>>
                     (productFeatures);

            #endregion


            return result;

            });
        }
    }
}
