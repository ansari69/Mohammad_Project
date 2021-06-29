using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductColors.Commands.UpsertProductColor
{
   public class UpsertProductColorCommand
        : IRequest<UpsertProductColorVM>
    {
        public string ProductColorId { get; set; }
        public string PersianName { get; set; }
        public string LatinName { get; set; }
        public string ColorCode { get; set; }
    }
}
