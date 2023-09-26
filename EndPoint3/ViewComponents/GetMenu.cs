using Application.Services.Common.Queries.GetMenuItem;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.ViewComponents
{
    public class GetMenu:ViewComponent
    {
        private readonly IGetMenuItemService _getMenuItemService;

        public GetMenu(IGetMenuItemService getMenuItemService)
        {
            _getMenuItemService = getMenuItemService;
        }

        public IViewComponentResult Invoke()
        {
            var menuItems = _getMenuItemService.Execute().Data;
            return View(viewName:"GetMenu",menuItems);
        }
    }
}
