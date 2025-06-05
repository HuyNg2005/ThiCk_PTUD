namespace QuanLyNhaHang.Dtos.BookingDetail;

public class BookingSummaryDto
{
    public Guid BookingId { get; set; }
    public string RoomName { get; set; }
    public decimal RoomPricePerNight { get; set; }
    public int TotalNights { get; set; }
    public decimal RoomTotalPrice { get; set; }
    public decimal ServiceTotalPrice { get; set; }
    public decimal TotalPrice { get; set; }
}