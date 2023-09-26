using Application.Services.Carts;
using EndPoint.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.ViewComponents
{
    public class Cart:ViewComponent
    {
        private readonly ICartService _cartService;
        private readonly CookiesManeger _cookiesManeger;
        public Cart(ICartService cartService)
        {
            _cartService = cartService;
            _cookiesManeger= new CookiesManeger();
        }

        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);

            return View(viewName:"Cart",model:_cartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext),userId).Data);
        }
    }
}
