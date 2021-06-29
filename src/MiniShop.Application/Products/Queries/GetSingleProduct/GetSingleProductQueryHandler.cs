using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Products.Queries.GetSingleProduct
{
    public class GetSingleProductQueryHandler
        : IRequestHandler<GetSingleProductQuery, GetSingleProductVM>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetSingleProductQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetSingleProductVM> Handle(GetSingleProductQuery request, CancellationToken cancellationToken)
        {

            var result = new GetSingleProductVM();

            var product = await _context.Products.AsNoTracking()
                .Include(e=>e.SelectedColorProducts).ThenInclude(e=>e.ProductColor)
                .Include(e => e.ProductFeaturesValues).ThenInclude(e => e.ProductFeature)
                .Include(e => e.ProductType)
                .Include(e => e.ProductPosition)
                .Include(e=>e.ProductLikes)
                .SingleOrDefaultAsync(t => t.ProductId == request.ProductId);

            if (product == null || !product.IsActive || !product.IsConfirm)
                throw new EntryValidationException();

            //Mapping Product
            result = _mapper.Map<Product, GetSingleProductVM>(product);


            // Like count
            if(product.ProductLikes.Any())
            {
                result.LikeCount = product.ProductLikes.Count(e => e.IsLike);
                result.DisLikeCount = product.ProductLikes.Count(e => !e.IsLike);
            }

            //Mapping color
            result.Colors = _mapper.Map<IEnumerable<SelectedColorProduct>,IEnumerable<ProductColorDTO>>
                    (product.SelectedColorProducts);

            //Mapping Feature
            result.Features = _mapper.Map<IEnumerable<ProductFeaturesValue>, IEnumerable<ProductFeaturesValueDTO>>
                    (product.ProductFeaturesValues);

            return result;
        }
    }
}
