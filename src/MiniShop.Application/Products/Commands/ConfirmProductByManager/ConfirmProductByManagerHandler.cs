using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Products.Commands.ConfirmProductByManager
{
    public class ConfirmProductByManagerHandler
        : IRequestHandler<ConfirmProductByManagerCommand, bool>
    {
        private readonly IAppDbContext _context;

        public ConfirmProductByManagerHandler(IAppDbContext context)
        {
            _context = context;
        }


        public async Task<bool> Handle(ConfirmProductByManagerCommand request, CancellationToken cancellationToken)
        {

            var product = await _context.Products
               .SingleOrDefaultAsync(e => e.ProductId == request.ProductId);

            if (product == null || product.IsConfirm)
                throw new EntryValidationException();

            product.IsConfirm = true;

            await _context.SaveChangesAsync();


            return true;
        }
    }
}
