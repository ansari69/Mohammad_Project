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

namespace MiniShop.Application.ProductColors.Queries.GetAllProductColors
{
    public class GetAllProductColorsQueryHandler
        : IRequestHandler<GetAllProductColorsQuery, GetAllProductColorsVM>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllProductColorsQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetAllProductColorsVM> Handle(GetAllProductColorsQuery request, CancellationToken cancellationToken)
        {

            return await Task.Run(() =>
            {

                //getting as iqueryable
                IQueryable<ProductColor> productColors = _context.ProductColors.AsNoTracking()
                .Where(b => b.IsActive == true);


                //Filtering By: FeatureName
                if (!String.IsNullOrEmpty(request.SearchValue))
                    productColors = productColors.Where(t => t.PersianName.Contains(request.SearchValue) 
                                      || t.LatinName.Contains(request.SearchValue)
                                      || t.ColorCode.Contains(request.SearchValue)
                                      );


                var totalResults = productColors.Count();
                var totalPage = productColors.TotalPagesCount(request.EachPerPage);

                //Fail if no result found
                if (totalPage == 0)
                {
                    return new GetAllProductColorsVM()
                    {
                        TotalPages = totalPage,
                        PageId = request.PageId,
                        TotalResults = totalResults,
                        ProductColors = new List<ProductColorVM>()
                    };
                }

                //if pageId is larger than total page return last page (cuz that make no sense)
                if (request.PageId > totalPage) request.PageId = totalPage;

                //paging
                var fproductColorsResult =
                    productColors.ToPaginatedQuery(request.PageId, request.EachPerPage).ToList();

                //mapping
                var listResult = _mapper.Map<IEnumerable<ProductColor>, IEnumerable<ProductColorVM>>(fproductColorsResult);

                //Creating result
                var result = new GetAllProductColorsVM()
                {
                    PageId = request.PageId,
                    TotalPages = totalPage,
                    TotalResults = totalResults,
                    ProductColors = listResult.ToList()
                };

                return result;

            });

        }
    }
}
