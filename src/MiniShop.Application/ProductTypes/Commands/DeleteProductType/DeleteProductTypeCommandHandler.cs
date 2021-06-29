using MediatR;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.ProductTypes.Commands.DeleteProductType
{
    public class DeleteProductTypeCommandHandler
         : IRequestHandler<DeleteProductTypeCommand, bool>
    {

        private readonly IAppDbContext _context;

        public DeleteProductTypeCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductTypeCommand request, CancellationToken cancellationToken)
        {

            var productType = await _context.ProductTypes.FindAsync(request.ProductTypeId);

            if (productType == null || !productType.IsActive)
                throw new EntryValidationException();

            productType.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
