namespace QuanLyNhaHang.Entities
{
    public class BookingServiceDetailEntity
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public Guid ServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }  
        public decimal TotalPrice { get; set; }
        // Navigation
        public BookingEntity Booking { get; set; }
        public ServiceHotelEntity Service { get; set; }
    }
}
