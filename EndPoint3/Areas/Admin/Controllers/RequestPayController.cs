using Application.Services.Fainances.Queries.GetRequestPayForAdmin;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RequestPayController : Controller
    {
        private readonly IGetRequestPayForAdminService _getRequestPayForAdminService;

        public RequestPayController(IGetRequestPayForAdminService getRequestPayForAdminService)
        {
            _getRequestPayForAdminService = getRequestPayForAdminService;
        }
        public IActionResult Index()
        {
            return View(_getRequestPayForAdminService.Execute().Data);
        }
    }
}
