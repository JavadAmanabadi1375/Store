using Application.Interfaces.Contexts;
using Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Common.Queries.GetMenuItem
{
    public interface IGetMenuItemService
    {
        ResultDto<List<MenuItemDto>> Execute();
    }

    public class GetMenuItemService : IGetMenuItemService
    {
        private readonly IDataBaseContext _context;

        public GetMenuItemService(IDataBaseContext context)
        {
            _context = context;
        }


        public ResultDto<List<MenuItemDto>> Execute()
        {
            var categories = _context.Categories.Include(x => x.SubCategories)
                .Where(y => y.ParentCategoryId == null)
                .Select(z => new MenuItemDto()
                {
                    CatId = z.Id,
                    Name = z.Name,
                    Child = z.SubCategories.Select(o => new MenuItemDto()
                    {
                        CatId = o.Id,
                        Name = o.Name,
                    }).ToList()
                }).ToList();

            var result= new ResultDto<List<MenuItemDto>>()
            {
                Data = categories,
                IsSuccess = true,
                Message=""
            };

            return result;
        }
    }
    public class MenuItemDto
    {
        public long CatId { get; set; }
        public string Name { get; set; }
        public List<MenuItemDto> Child { get; set; }
    }
}
