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

namespace MiniShop.Application.ProductColors.Queries.GetSingleProductColor
{
    public class GetSingleProductColorQueryHandler
        : IRequestHandler<GetSingleProductColorQuery, ProductColorVM>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetSingleProductColorQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductColorVM> Handle(GetSingleProductColorQuery request, CancellationToken cancellationToken)
        {

            var productColor = await _context.ProductColors.AsNoTracking()
               .SingleOrDefaultAsync(t => t.ProductColorId == request.ProductColorId);

            if (productColor == null || !productColor.IsActive)
                throw new EntryValidationException();

            //Mapping
            var result = _mapper.Map<ProductColor, ProductColorVM>(productColor);

            return result;


        }
    }
}
