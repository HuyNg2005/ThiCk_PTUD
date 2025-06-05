using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaHang.Dtos.User
{
    public class UserRegisterRequest
    {
   
        public string UserName { get; set; }

       
        public string Password { get; set; }

     
        public string FullName { get; set; }

      
        [EmailAddress]
        public string Email { get; set; }
    }
}
