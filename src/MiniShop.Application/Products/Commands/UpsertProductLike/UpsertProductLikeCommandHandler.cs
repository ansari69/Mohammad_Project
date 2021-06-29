using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Products.Commands.UpsertProductLike
{
    public class UpsertProductLikeCommandHandler
         : IRequestHandler<UpsertProductLikeCommand, bool>
    {
        private readonly IAppDbContext _context;

        public UpsertProductLikeCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpsertProductLikeCommand request, CancellationToken cancellationToken)
        {

            var isValid = await _context.Products.AsNoTracking()               
                .AnyAsync(e => e.ProductId == request.ProductId && e.IsActive && e.IsConfirm);

            if (!isValid)
                throw new EntryValidationException();

            var productLikeModel = await _context.ProductLikes
                .Where(e => e.ProductId == request.ProductId && e.CreatorId == request.CreatorId)
                   .FirstOrDefaultAsync();

            // Insert new
            if(productLikeModel == null)
            {
                var productLike = new ProductLike();

                productLike.ProductLikeId = Guid.NewGuid().ToString();
                productLike.CreatorId = request.CreatorId;
                productLike.IsLike = request.IsLike;
                productLike.ProductId = request.ProductId;

                _context.ProductLikes.Add(productLike);

            }
            else
            {
                if(request.IsLike)
                {
                    if(productLikeModel.IsLike)
                        _context.ProductLikes.Remove(productLikeModel);
                    else
                        productLikeModel.IsLike = request.IsLike;                  
                }
                else
                {
                    if (!productLikeModel.IsLike)
                        _context.ProductLikes.Remove(productLikeModel);
                    else
                        productLikeModel.IsLike = request.IsLike;
                }

            }


            await _context.SaveChangesAsync();

            return true;
        }
    }
}
