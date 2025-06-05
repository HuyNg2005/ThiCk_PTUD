namespace QuanLyNhaHang.Dtos.Booking
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public string RoomName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public decimal? TotalPrice { get; set; }
        public string CheckInByUser { get; set; }
        public string CheckOutByUser { get; set; }
        public List<string> CustomerNames { get; set; }
    }
}
