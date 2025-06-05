namespace QuanLyNhaHang.Entities
{
    public class BookingDetailEntity
    {
        public Guid Id { get; set; }

        public Guid BookingId { get; set; }
        public Guid CustomerId { get; set; }

        // Navigation
        public BookingEntity Booking { get; set; }
        public CustomerEntity Customer { get; set; }
    }
}
