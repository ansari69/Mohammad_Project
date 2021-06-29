using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Helpers.ListFilterExtentions;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Application.Common.Models.ViewModels;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.ProductTypes.Queries.GetAllProductTypes
{
    public class GetAllProductTypesQueryHandler
        : IRequestHandler<GetAllProductTypesQuery, GetAllProductTypesVM>
    {

        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllProductTypesQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<GetAllProductTypesVM> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {

            return await Task.Run(() =>
            {


                //getting as iqueryable
                IQueryable<ProductType> productTypes = _context.ProductTypes.AsNoTracking()
                .Where(b => b.IsActive == true);


            //Filtering By: ProductTypeName, Description
            if (!String.IsNullOrEmpty(request.SearchValue))
                productTypes = productTypes.Where(t => t.ProductTypeName.Contains(request.SearchValue) ||
                                             t.Description.Contains(request.SearchValue));


            var totalResults = productTypes.Count();
            var totalPage = productTypes.TotalPagesCount(request.EachPerPage);

            //Fail if no result found
            if (totalPage == 0)
            {
                return new GetAllProductTypesVM()
                {
                    TotalPages = totalPage,
                    PageId = request.PageId,
                    TotalResults = totalResults,
                    ProductTypes = new List<ProductTypeVM>()
                };
            }

            //if pageId is larger than total page return last page (cuz that make no sense)
            if (request.PageId > totalPage) request.PageId = totalPage;

            //paging
            var productTypesResult =
                productTypes.ToPaginatedQuery(request.PageId, request.EachPerPage).ToList();

            //mapping
            var listResult = _mapper.Map<IEnumerable<ProductType>, IEnumerable<ProductTypeVM>>(productTypesResult);

            //Creating result
            var result = new GetAllProductTypesVM()
            {
                PageId = request.PageId,
                TotalPages = totalPage,
                TotalResults = totalResults,
                ProductTypes = listResult.ToList()
            };

            return result;

                //  throw new NotImplementedException();
            });
        }
    }
}
