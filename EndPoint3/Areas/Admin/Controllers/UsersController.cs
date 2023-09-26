using Application.Services.Users.Commands.RegisterUser;
using Application.Services.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.Services.Users.Queries.GetRoles;
using System.Collections.Generic;
using Application.Services.Users.Commands.EditUser;
using Application.Services.Users.Commands.RemoveUser;
using Application.Services.Users.Commands.UserSatusChange;
using Microsoft.AspNetCore.Authorization;

namespace EndPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IGetUsersService _getUsersService;
        private readonly IGetRolesService _getRolesService;
        private readonly IRegisterUserService _registerUserService;
        private readonly IRemoveUserService _removeUserService;
        private readonly IUserSatusChangeService _userSatusChangeService;
        private readonly IEditUserService _editUserService;
        public UsersController(IGetUsersService getUsersService,
            IGetRolesService getRolesService,
            IRegisterUserService registerUserService,
            IRemoveUserService removeUserService,
            IUserSatusChangeService userSatusChangeService,
            IEditUserService editUserService)
        {
            _getUsersService = getUsersService;   
            _getRolesService = getRolesService;
            _registerUserService= registerUserService;
            _removeUserService = removeUserService;
            _userSatusChangeService = userSatusChangeService;
            _editUserService = editUserService;
        }

        public IActionResult Index(string searchKey, int pageNumber)
        {
            return View(_getUsersService.Execute(new RequestGetUserDto()
            {
                PageNumber = pageNumber,
                SearchKey= searchKey,
            }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_getRolesService.Execute().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(string email, string fullName, long roleId, string password, string rePassword)
        {
            var result = _registerUserService.Execute(new RequestRegisterUserDto()
            {
                Email = email,
                FullName = fullName,
                Roles = new List<RolesInRegisterUserDto>()
                {
                    new RolesInRegisterUserDto()
                    {
                        Id = roleId,
                    }
                },
                Password = password,
                RePassword = rePassword
            });

            return Json(result);
        }


        [HttpPost]
        public IActionResult Delete(long UserId)
        {
            return Json(_removeUserService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult UserSatusChange(long UserId)
        {
            return Json(_userSatusChangeService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult Edit(long UserId, string Fullname)
        {
            return Json(_editUserService.Execute(new RequestEdituserDto
            {
                Fullname = Fullname,
                UserId = UserId,
            }));
        }
    }
}
