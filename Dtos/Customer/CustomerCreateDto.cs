namespace QuanLyNhaHang.Dtos.Customer
{
    public class CustomerCreateDto
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdCard { get; set; }
    }
}
