using Application.Interfaces.FacadPatterns;
using Application.Services.Common.Queries.GetHomePageImages;
using Application.Services.Common.Queries.GetSlider;
using Application.Services.Products.Queries.GetProductForSite;
using EndPoint.Models;
using EndPoint.Models.ViewModels.HomePages;
using EndPoint.Models.ViewModels.HomePages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EndPoint.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetSliderService _getSliderService;
        private readonly IGetHomePageImagesService _getHomePageImagesService;
        private readonly IProductFacad _productFacad;
        public HomeController(ILogger<HomeController> logger, IGetSliderService getSliderService
            , IGetHomePageImagesService getHomePageImagesService,
            IProductFacad productFacad)
        {
            _logger = logger;
            _getSliderService = getSliderService;
            _getHomePageImagesService = getHomePageImagesService;
            _productFacad = productFacad;
        }
        
        public IActionResult Index()
        {
            HomePageViewModel viewModel = new HomePageViewModel()
            {
                Sliders = _getSliderService.Execute().Data,
                PageImages=_getHomePageImagesService.Execute().Data,
                Camera=_productFacad.GetProductForSiteService.Execute(Ordering.Newest,
                null,1,6,14).Data.Products
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
