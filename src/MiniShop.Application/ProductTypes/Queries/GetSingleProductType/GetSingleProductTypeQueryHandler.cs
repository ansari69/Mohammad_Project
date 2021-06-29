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

namespace MiniShop.Application.ProductTypes.Queries.GetSingleProductType
{
    public class GetSingleProductTypeQueryHandler
        : IRequestHandler<GetSingleProductTypeQuery, ProductTypeVM>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetSingleProductTypeQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ProductTypeVM> Handle(GetSingleProductTypeQuery request, CancellationToken cancellationToken)
        {

            var type = await _context.ProductTypes.AsNoTracking()
                .SingleOrDefaultAsync(t => t.ProductTypeId == request.ProductTypeId);

            if (type == null || !type.IsActive)
                throw new EntryValidationException();

            //Mapping
            var result = _mapper.Map<ProductType, ProductTypeVM>(type);

            return result;

        }
    }
}
