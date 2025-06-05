namespace QuanLyNhaHang.Dtos.BookingServiceDetail;

public class BookingDetailServiceUpdateDto
{
    public Guid BookingId { get; set; }
    public Guid ServiceHotelId { get; set; }
    public int Quantity { get; set; }
}