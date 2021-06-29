using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Products.Queries.GetImageProduct
{
    public class GetImageProductQueryHandler
        : IRequestHandler<GetImageProductQuery, string>
    {

        private readonly IAppDbContext _context;

        public GetImageProductQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetImageProductQuery request, CancellationToken cancellationToken)
        {


         var product = await _context.Products.AsNoTracking()
         .SingleOrDefaultAsync(t => t.ProductId == request.ProductId);

            if (product == null || !product.IsActive || !product.IsConfirm)
                throw new EntryValidationException();

            string imageName = null;

            if(product.ImageName != null)
            {

                imageName = product.ImageName;
            }



            return imageName;
        }
    }
}
