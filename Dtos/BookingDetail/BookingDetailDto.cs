namespace QuanLyNhaHang.Dtos.BookingDetailDto
{
    public class BookingDetailDto
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerFullName { get; set; }
    }
}
