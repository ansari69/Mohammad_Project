using AutoMapper;
using MediatR;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.ProductFeatures.Commands.UpsertProductFeature
{
    public class UpsertProductFeatureCommandHandler
        : IRequestHandler<UpsertProductFeatureCommand, UpsertProductFeatureVM>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UpsertProductFeatureCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpsertProductFeatureVM> Handle(UpsertProductFeatureCommand request, CancellationToken cancellationToken)
        {

            var productFeature = new ProductFeature();

            // Insert
            if (request.ProductFeatureId == null)
            {
                productFeature.ProductFeatureId = Guid.NewGuid().ToString();

                productFeature.FeatureName = request.FeatureName;

                productFeature.IsActive = true;

                _context.ProductFeatures.Add(productFeature);
            }
            // Update
            else
            {
                productFeature = await _context.ProductFeatures.FindAsync(request.ProductFeatureId);

                if (productFeature == null || !productFeature.IsActive)
                    throw new EntryValidationException();

            }


            // Mapping
            productFeature = _mapper.Map<UpsertProductFeatureCommand, ProductFeature>(request, productFeature);

            //Save
            await _context.SaveChangesAsync();


            return new UpsertProductFeatureVM
            {
                ProductFeatureId = productFeature.ProductFeatureId
            };

        }
    }
}
