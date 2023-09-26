using Domain.Entities.Commons;

namespace Domain.Entities.Users
{
    public class UserInRole:BaseEntity
    {
        public User User { get; set; }
        public long UserId { get; set; }
        public Role Role { get; set; }
        public long RoleId { get; set; }

    }
}
