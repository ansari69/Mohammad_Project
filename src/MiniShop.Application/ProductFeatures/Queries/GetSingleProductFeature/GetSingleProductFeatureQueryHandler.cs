using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Application.Common.Models.ViewModels;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.ProductFeatures.Queries.GetSingleProductFeature
{
   public class GetSingleProductFeatureQueryHandler
        : IRequestHandler<GetSingleProductFeatureQuery, ProductFeatureVM>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetSingleProductFeatureQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductFeatureVM> Handle(GetSingleProductFeatureQuery request, CancellationToken cancellationToken)
        {

            var productFeature = await _context.ProductFeatures.AsNoTracking()
                .SingleOrDefaultAsync(t => t.ProductFeatureId == request.ProductFeatureId);

            if (productFeature == null || !productFeature.IsActive)
                throw new EntryValidationException();

            //Mapping
            var result = _mapper.Map<ProductFeature, ProductFeatureVM>(productFeature);

            return result;

        }
    }
}
