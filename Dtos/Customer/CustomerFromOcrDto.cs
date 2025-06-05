namespace QuanLyNhaHang.Dtos.Customer;

public class CustomerFromOcrDto
{
    public string FullName { get; set; }
    public string IdCard { get; set; }
    public string DateOfBirth { get; set; } // string để dễ hiển thị, xử lý sau
}