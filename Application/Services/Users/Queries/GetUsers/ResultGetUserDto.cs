using System.Collections.Generic;

namespace Application.Services.Users.Queries.GetUsers
{
    public class ResultGetUserDto
    {
        public List<GetUsersDto> Users { get; set; }

        public int Rows { get; set; }

    }
}
