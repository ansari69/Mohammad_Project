using MediatR;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.ProductFeatures.Commands.DeleteProductFeature
{
    public class DeleteProductFeatureCommandHandler
         : IRequestHandler<DeleteProductFeatureCommand, bool>
    {

        private readonly IAppDbContext _context;

        public DeleteProductFeatureCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteProductFeatureCommand request, CancellationToken cancellationToken)
        {
            var productFeature = await _context.ProductFeatures.FindAsync(request.ProductFeatureId);

            if (productFeature == null || !productFeature.IsActive)
                throw new EntryValidationException();

            productFeature.IsActive = false;

            await _context.SaveChangesAsync();

            return true;


        }
    }
}
