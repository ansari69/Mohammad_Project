using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Commands.UpsertProductLike
{
    public class UpsertProductLikeCommand
        : IRequest<bool>
    {
        public string ProductId { get; set; }
        public bool IsLike { get; set; }
        public string CreatorId { get; set; }



    }
}
