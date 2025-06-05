using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaHang.Dtos.User
{
    public class UserLoginRequest
    {
        
       public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
