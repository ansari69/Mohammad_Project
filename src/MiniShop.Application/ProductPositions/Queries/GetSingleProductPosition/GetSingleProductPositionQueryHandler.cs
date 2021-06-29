using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Application.Common.Models.ViewModels;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.ProductPositions.Queries.GetSingleProductPosition
{
    public class GetSingleProductPositionQueryHandler
        : IRequestHandler<GetSingleProductPositionQuery, ProductPositionVM>
    {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetSingleProductPositionQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ProductPositionVM> Handle(GetSingleProductPositionQuery request, CancellationToken cancellationToken)
        {
            var productPosition = await _context.ProductPositions.AsNoTracking()
                .SingleOrDefaultAsync(t => t.ProductPositionId == request.ProductPositionId);

            if (productPosition == null || !productPosition.IsActive)
                throw new Exception();

            //Mapping
            var result = _mapper.Map<ProductPosition, ProductPositionVM>(productPosition);

            return result;

        }
    }
}
