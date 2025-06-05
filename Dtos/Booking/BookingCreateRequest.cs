using QuanLyNhaHang.Dtos.Customer;

namespace QuanLyNhaHang.Dtos.Booking;

public class BookingCreateRequestDto
{
    public Guid RoomId { get; set; }
    public Guid? CheckInByUserId { get; set; }
    public DateTime CheckIn { get; set; }
    
}

