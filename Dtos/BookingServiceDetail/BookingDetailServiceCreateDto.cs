namespace QuanLyNhaHang.Dtos.BookingServiceDetail
{
    public class BookingDetailServiceCreateDto
    {
        public Guid BookingId { get; set; }
        public Guid ServiceId { get; set; }
        public int Quantity { get; set; }
    }
}
