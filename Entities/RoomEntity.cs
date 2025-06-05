namespace QuanLyNhaHang.Entities
{
    public class RoomEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public RoomType Type { get; set; }
        public RoomStatus Status { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        // Navigation
        public ICollection<BookingEntity> Bookings { get; set; }= new List<BookingEntity>();

        public enum RoomType
        {
            Single,
            Double,
            Standard,
            VIP
        }

        public enum RoomStatus
        {
            Available,
            Booked,
            Maintenance
        }
    }
}
