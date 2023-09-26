using Application.Interfaces.Contexts;
using Common;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;

        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultGetUserDto Execute(RequestGetUserDto request)
        {
            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                users=users.Where(x=>x.FullName.Contains(request.SearchKey) || x.Email.Contains(request.SearchKey));
            }

            int rowsCount = 0;
            var usersList= users.ToPaged(request.PageNumber,20, out rowsCount).Select(x=>new GetUsersDto()
            {
                Email=x.Email,
                FullName=x.FullName,
                IsActive=x.IsActive,
                Id=x.Id,
            }).ToList();

            return new ResultGetUserDto()
            {
                Users = usersList,
                Rows = rowsCount
            };
        }
    }
}
