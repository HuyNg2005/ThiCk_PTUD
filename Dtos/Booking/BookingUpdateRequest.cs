using QuanLyNhaHang.Dtos.BookingServiceDetailDto;
using QuanLyNhaHang.Dtos.Customer;
using QuanLyNhaHang.Dtos.Room;

namespace QuanLyNhaHang.Dtos.Booking;

public class BookingUpdateRequest
{
    public DateTime? CheckOutDate { get; set; }
    public List<CustomerUpdateDto> Customers { get; set; } = new();
    
}