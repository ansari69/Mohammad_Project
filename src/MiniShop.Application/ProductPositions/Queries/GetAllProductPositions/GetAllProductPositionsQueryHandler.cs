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

namespace MiniShop.Application.ProductPositions.Queries.GetAllProductPositions
{
    public class GetAllProductPositionsQueryHandler
        : IRequestHandler<GetAllProductPositionsQuery, GetAllProductPositionsVM>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllProductPositionsQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetAllProductPositionsVM> Handle(GetAllProductPositionsQuery request, CancellationToken cancellationToken)
        {

            return await Task.Run(() =>
            {

                //getting as iqueryable
                IQueryable<ProductPosition> productPositions = _context.ProductPositions.AsNoTracking()
                .Where(b => b.IsActive == true);


                //Filtering By: ProductPositionName
                if (!String.IsNullOrEmpty(request.SearchValue))
                productPositions = productPositions.Where(t => t.ProductPositionName.Contains(request.SearchValue));



                var totalResults = productPositions.Count();
            var totalPage = productPositions.TotalPagesCount(request.EachPerPage);

            //Fail if no result found
            if (totalPage == 0)
            {
                return new GetAllProductPositionsVM()
                {
                    TotalPages = totalPage,
                    PageId = request.PageId,
                    TotalResults = totalResults,
                    ProductPositions = new List<ProductPositionVM>()
                };
            }

            //if pageId is larger than total page return last page (cuz that make no sense)
            if (request.PageId > totalPage) request.PageId = totalPage;

            //paging
            var productsResult =
                productPositions.ToPaginatedQuery(request.PageId, request.EachPerPage).ToList();

            //mapping
            var listResult = _mapper.Map<IEnumerable<ProductPosition>, IEnumerable<ProductPositionVM>>(productsResult);

            //Creating result
            var result = new GetAllProductPositionsVM()
            {
                PageId = request.PageId,
                TotalPages = totalPage,
                TotalResults = totalResults,
                ProductPositions = listResult.ToList()
            };

            return result;

            });

        }
    }
}
