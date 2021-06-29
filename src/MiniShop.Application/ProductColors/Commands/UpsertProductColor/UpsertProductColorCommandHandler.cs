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

namespace MiniShop.Application.ProductColors.Commands.UpsertProductColor
{
    public class UpsertProductColorCommandHandler
         : IRequestHandler<UpsertProductColorCommand, UpsertProductColorVM>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UpsertProductColorCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpsertProductColorVM> Handle(UpsertProductColorCommand request, CancellationToken cancellationToken)
        {


            var productFeature = new ProductColor();

            // Insert
            if (request.ProductColorId == null)
            {
                productFeature.ProductColorId = Guid.NewGuid().ToString();

                productFeature.IsActive = true;

                _context.ProductColors.Add(productFeature);
            }
            // Update
            else
            {
                productFeature = await _context.ProductColors.FindAsync(request.ProductColorId);

                if (productFeature == null || !productFeature.IsActive)
                    throw new EntryValidationException();

            }


            // Mapping
            productFeature = _mapper.Map<UpsertProductColorCommand, ProductColor>(request, productFeature);

            //Save
            await _context.SaveChangesAsync();


            return new UpsertProductColorVM
            {
                ProductColorId = productFeature.ProductColorId
            };


          //  throw new NotImplementedException();
        }
    }
}
