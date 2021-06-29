using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniShop.Application.Common.Exceptions;
using MiniShop.Application.Common.Helpers.ListFilterExtentions;
using MiniShop.Application.Common.Interfaces;
using MiniShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniShop.Application.Products.Queries.GetProductsByFilter
{
   public class GetProductsByFilterQueryHandler
        : IRequestHandler<GetProductsByFilterQuery, GetProductsByFilterVM>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsByFilterQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetProductsByFilterVM> Handle(GetProductsByFilterQuery request, CancellationToken cancellationToken)
        {

            return await Task.Run(() =>
            {

                //getting as iqueryable
                IQueryable<Product> products = _context.Products.AsNoTracking()
                .Include(e=>e.ProductPosition)
                .Include(e=>e.ProductType)
                .OrderByDescending(e => e.CreateDate)
                .Where(b => b.IsActive == true && b.IsConfirm);


                //Filtering By: Product Name, Description, ProductDisplayName
                if (!String.IsNullOrEmpty(request.SearchValue))
                    products = products.Where(t => t.ProductName.Contains(request.SearchValue) ||
                                                 t.Description.Contains(request.SearchValue)||
                                                 t.ProductDisplayName.Contains(request.SearchValue)
                                                 );

                 if (!String.IsNullOrEmpty(request.ProductPositionId))
                    products = products.Where(e => e.ProductPositionId == request.ProductPositionId);

                if (!String.IsNullOrEmpty(request.ProductTypeId))
                    products = products.Where(e => e.ProductTypeId == request.ProductTypeId);

                //Sorting
                if (!String.IsNullOrEmpty(request.SortBy))
                {
                    try
                    { products = products.FilterOrderBy(request.SortBy, request.SortByDescending); }
                    catch
                    { throw new OperationFailedException(); }
                }



                var totalResults = products.Count();
                var totalPage = products.TotalPagesCount(request.EachPerPage);

                //Fail if no result found
                if (totalPage == 0)
                {
                    return new GetProductsByFilterVM()
                    {
                        TotalPages = totalPage,
                        PageId = request.PageId,
                        TotalResults = totalResults,
                        Products = new List<ShowProductsByFilterVM>()
                    };
                }

                //if pageId is larger than total page return last page (cuz that make no sense)
                if (request.PageId > totalPage) request.PageId = totalPage;

                //paging
                var productTypesResult =
                    products.ToPaginatedQuery(request.PageId, request.EachPerPage).ToList();

                //mapping
                var listResult = _mapper.Map<IEnumerable<Product>, IEnumerable<ShowProductsByFilterVM>>(productTypesResult);

                //Creating result
                var result = new GetProductsByFilterVM()
                {
                    PageId = request.PageId,
                    TotalPages = totalPage,
                    TotalResults = totalResults,
                    Products = listResult.ToList()
                };

                return result;

            });

        }
    }
}
