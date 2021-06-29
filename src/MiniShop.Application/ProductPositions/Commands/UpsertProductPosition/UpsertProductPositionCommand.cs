using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductPositions.Commands.UpsertProductPosition
{
    public class UpsertProductPositionCommand
        : IRequest<UpsertProductPositionVM>
    {
        public string ProductPositionId { get; set; }
        public string ProductPositionName { get; set; }
    }
}
