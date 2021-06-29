using MediatR;
using MiniShop.Application.Common.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.ProductTypes.Commands.UpsertProductType
{
   public class UpsertProductTypeCommand : IRequest<UpsertProductTypeVM>
    {
        public string ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public string Description { get; set; }

    }
}
