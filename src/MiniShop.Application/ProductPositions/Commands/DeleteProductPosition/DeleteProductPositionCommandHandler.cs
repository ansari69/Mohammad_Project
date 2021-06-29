using MediatR;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.ProductPositions.Commands.DeleteProductPosition
{
    public class DeleteProductPositionCommandHandler
        : IRequestHandler<DeleteProductPositionCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteProductPositionCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductPositionCommand request, CancellationToken cancellationToken)
        {

            var productPositions = await _context.ProductPositions.FindAsync(request.ProductPositionId);

            if (productPositions == null || !productPositions.IsActive)
                throw new EntryValidationException();

            productPositions.IsActive = false;

            await _context.SaveChangesAsync();

            return true;

        }
    }
}
