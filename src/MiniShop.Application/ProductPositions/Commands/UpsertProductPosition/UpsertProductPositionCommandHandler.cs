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

namespace MiniShop.Application.ProductPositions.Commands.UpsertProductPosition
{
    public class UpsertProductPositionCommandHandler
        : IRequestHandler<UpsertProductPositionCommand, UpsertProductPositionVM>
    {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public UpsertProductPositionCommandHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpsertProductPositionVM> Handle(UpsertProductPositionCommand request, CancellationToken cancellationToken)
        {


            var productPosition = new ProductPosition();

            // Insert
            if (request.ProductPositionId == null)
            {
                productPosition.ProductPositionId = Guid.NewGuid().ToString();

                productPosition.IsActive = true;

                _context.ProductPositions.Add(productPosition);
            }
            // Update
            else
            {
                productPosition = await _context.ProductPositions.FindAsync(request.ProductPositionId);

                if (productPosition == null || !productPosition.IsActive)
                    throw new EntryValidationException();

            }



            productPosition = _mapper.Map<UpsertProductPositionCommand, ProductPosition>(request, productPosition);

            await _context.SaveChangesAsync();


            return new UpsertProductPositionVM
            {
                ProductPositionId = productPosition.ProductPositionId
            };


           // throw new NotImplementedException();
        }
    }
}
