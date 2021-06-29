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

namespace MiniShop.Application.ProductFeatures.Queries.GetAllProductFeatures
{
    public class GetAllProductFeaturesQueryHandler
        : IRequestHandler<GetAllProductFeaturesQuery, GetAllProductFeaturesVM>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetAllProductFeaturesQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetAllProductFeaturesVM> Handle(GetAllProductFeaturesQuery request, CancellationToken cancellationToken)
        {

            return await Task.Run(() =>
            {

                //getting as iqueryable
                IQueryable<ProductFeature> productFeatures = _context.ProductFeatures.AsNoTracking()
                .Where(b => b.IsActive == true);


                //Filtering By: FeatureName
                if (!String.IsNullOrEmpty(request.SearchValue))
                    productFeatures = productFeatures.Where(t => t.FeatureName.Contains(request.SearchValue));



                var totalResults = productFeatures.Count();
                var totalPage = productFeatures.TotalPagesCount(request.EachPerPage);

                //Fail if no result found
                if (totalPage == 0)
                {
                    return new GetAllProductFeaturesVM()
                    {
                        TotalPages = totalPage,
                        PageId = request.PageId,
                        TotalResults = totalResults,
                        ProductFeatures = new List<ProductFeatureVM>()
                    };
                }

                //if pageId is larger than total page return last page (cuz that make no sense)
                if (request.PageId > totalPage) request.PageId = totalPage;

                //paging
                var featuresResult =
                    productFeatures.ToPaginatedQuery(request.PageId, request.EachPerPage).ToList();

                //mapping
                var listResult = _mapper.Map<IEnumerable<ProductFeature>, IEnumerable<ProductFeatureVM>>(featuresResult);

                //Creating result
                var result = new GetAllProductFeaturesVM()
                {
                    PageId = request.PageId,
                    TotalPages = totalPage,
                    TotalResults = totalResults,
                    ProductFeatures = listResult.ToList()
                };

                return result;

            });


        }
    }
}
