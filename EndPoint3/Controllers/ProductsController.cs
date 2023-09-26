using Application.Interfaces.FacadPatterns;
using Application.Services.Products.Queries.GetProductForSite;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductFacad _productFacad;

        public ProductsController(IProductFacad productFacad)
        {
            _productFacad = productFacad;   
        }
        public IActionResult Index(Ordering ordering,string SearchKey, long? CatId=null,int page=1, int pageSize=20)
        {
            
            return View(_productFacad.GetProductForSiteService.Execute(ordering,SearchKey,page,pageSize, CatId).Data);
        }

        public IActionResult Detail(int id)
        {
            return View(_productFacad.GetProductDetailForSiteService.Execute(id).Data);
        }
    }
}
