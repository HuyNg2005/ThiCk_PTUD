namespace QuanLyNhaHang.Entities
{
    public class ServiceHotelEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ServiceType Type { get; set; }
        public decimal? Price { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<BookingServiceDetailEntity> BookingServices { get; set; }
    }

    public enum ServiceType
    {
        Food,
        Drink,
        Laundry,
        Spa,
        Other
    }
}
