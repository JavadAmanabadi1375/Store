using Application.Services.Common.Queries.GetCategory;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.ViewComponents
{
    public class Search:ViewComponent
    {
        private readonly IGetCategoryService _getCategoryService;

        public Search(IGetCategoryService getCategoryService)
        {
            _getCategoryService = getCategoryService;
        }

        public IViewComponentResult Invoke()
        {
            return View(viewName: "Search", model: _getCategoryService.Execute().Data);
        }
    }
}
