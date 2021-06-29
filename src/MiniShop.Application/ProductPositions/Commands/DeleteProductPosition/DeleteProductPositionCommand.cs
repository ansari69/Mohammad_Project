using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductPositions.Commands.DeleteProductPosition
{
    public class DeleteProductPositionCommand
        : IRequest<bool>
    {
        public string ProductPositionId { get; set; }

    }
}
