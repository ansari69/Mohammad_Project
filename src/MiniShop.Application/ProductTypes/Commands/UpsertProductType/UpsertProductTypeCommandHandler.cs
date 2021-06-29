using AutoMapper;
using MediatR;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Application.Common.Models.ViewModels;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.ProductTypes.Commands.UpsertProductType
{
    public class UpsertProductTypeCommandHandler :
          IRequestHandler<UpsertProductTypeCommand, UpsertProductTypeVM>
    {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UpsertProductTypeCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<UpsertProductTypeVM> Handle(UpsertProductTypeCommand request, CancellationToken cancellationToken)
        {

            var productType = new ProductType();

            // Insert
            if (request.ProductTypeId == null)
            {
                productType.ProductTypeId = Guid.NewGuid().ToString();

                productType.IsActive = true;

                _context.ProductTypes.Add(productType);
            }
            // Update
            else
            {
                productType = await _context.ProductTypes.FindAsync(request.ProductTypeId);

                if (productType == null)
                    throw new EntryValidationException();

            }



            productType = _mapper.Map<UpsertProductTypeCommand, ProductType>(request, productType);

            await _context.SaveChangesAsync();


            return new UpsertProductTypeVM
            {
                ProductTypeId = productType.ProductTypeId
            };

        }
    }
}
