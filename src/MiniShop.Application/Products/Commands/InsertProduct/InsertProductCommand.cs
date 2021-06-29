using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniShop.Application.Products.Commands.InsertProduct
{
   public class InsertProductCommand
        : IRequest<InsertProductVM>
    {

        public string ProductName { get; set; }
        public string ProductDisplayName { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        // public string ImageAddress { get; set; }
        //  public string ImageName { get; set; }


        public string ProductTypeId { get; set; }
        public string ProductPositionId { get; set; }
        public string CreatorId { get; set; }

        public IEnumerable<string> ColorIds { get; set; }
        public IEnumerable<FeatureInsertDTO> Features { get; set; }

    }

    public class FeatureInsertDTO
    {
        public string ProductFeatureId { get; set; }
        public string FeaturesValue { get; set; }
    }
}
