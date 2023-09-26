using Application.Interfaces.Contexts;
using Common;
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Application.Services.Products.Queries.GetProductForSite
{
    public class GetProductForSiteService : IGetProductForSiteService
    {

        private readonly IDataBaseContext _context;
        public GetProductForSiteService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultProductForSiteDto> Execute(Ordering ordering,string searchKey, int Page,int pageSize, long? CatId)
        {
            int totalRow = 0;
            var poductQueriable = _context.Products
                .Include(p => p.ProductImages).AsQueryable();
            if (CatId != null)
            {
                poductQueriable = poductQueriable.Where(x => x.CategoryId == CatId ||
                x.Category.ParentCategoryId==CatId).AsQueryable();
            }

            if (!string.IsNullOrEmpty(searchKey))
            {
                poductQueriable = poductQueriable.Where(p => p.Name.Contains(searchKey) || p.Brand.Contains(searchKey)).AsQueryable();
            }

            switch (ordering)
            {
                case Ordering.NotOrder:
                    poductQueriable = poductQueriable.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostVisited:
                    poductQueriable = poductQueriable.OrderByDescending(p => p.ViewCount).AsQueryable();
                    break;
                case Ordering.BestSelling:
                    break;
                case Ordering.MostPopular:
                    break;
                case Ordering.Newest:
                    poductQueriable = poductQueriable.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.Cheapest:
                    poductQueriable = poductQueriable.OrderBy(p => p.Price).AsQueryable();
                    break;
                case Ordering.MostExpensive:
                    poductQueriable = poductQueriable.OrderByDescending(p => p.Price).AsQueryable();
                    break;
                default:
                    break;
            }

            var product =poductQueriable.ToPaged(Page, pageSize, out totalRow);

            Random rd = new Random();
            try
            {
                return new ResultDto<ResultProductForSiteDto>
                {
                    Data = new ResultProductForSiteDto
                    {
                        TotalRow = totalRow,
                        Page=Page,
                        PageSize = pageSize,
                        Products = product.Select(p => new ProductForSiteDto
                        {
                            Id = p.Id,
                            Star = rd.Next(1, 5),
                            Title = p.Name,
                            ImageSrc = p.ProductImages.FirstOrDefault() is null ? "" : p.ProductImages.FirstOrDefault().Src,
                            Price = p.Price
                        }).ToList(),
                    },
                    IsSuccess = true,
                };

            }
            catch (Exception ex)
            {

                return new ResultDto<ResultProductForSiteDto>();
            }
        }
    }

}
