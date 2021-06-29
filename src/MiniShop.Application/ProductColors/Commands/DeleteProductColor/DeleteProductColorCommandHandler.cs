using MediatR;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.ProductColors.Commands.DeleteProductColor
{
    public class DeleteProductColorCommandHandler
        : IRequestHandler<DeleteProductColorCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteProductColorCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductColorCommand request, CancellationToken cancellationToken)
        {

            var productColor = await _context.ProductColors.FindAsync(request.ProductColorId);

            if (productColor == null || !productColor.IsActive)
                throw new EntryValidationException();

            productColor.IsActive = false;

            await _context.SaveChangesAsync();

            return true;


        }
    }
}
