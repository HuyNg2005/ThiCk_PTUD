using static QuanLyNhaHang.Entities.UserEntity;

namespace QuanLyNhaHang.Dtos.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Role { get; set; }
    }
}
