using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaHang.Dtos.User
{
    public class UserCreateRequest
    {

        

        public Guid? Id { get; set; }
        public string? UserName { get; set; }

       
        public string? Password { get; set; }

        public string? FullName { get; set; }

       
        public string? Email { get; set; }

        public string? Role { get; set; }  // "Admin", "Manager", "Staff"
    }
}
