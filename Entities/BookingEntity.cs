namespace QuanLyNhaHang.Entities
{
    public class BookingEntity
    {
        public Guid Id { get; set; }

        public Guid? CheckInByUserId { get; set; }
        public Guid? CheckOutByUserId { get; set; }
        public Guid RoomId { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public UserEntity CheckInByUser { get; set; }
        public UserEntity CheckOutByUser { get; set; }
        public RoomEntity Room { get; set; }

        public ICollection<BookingDetailEntity> BookingDetails { get; set; } = new List<BookingDetailEntity>();
        public ICollection<BookingServiceDetailEntity> BookingServices { get; set; } = new List<BookingServiceDetailEntity>();
        
    }
}
