namespace QuanLyNhaHang.Dtos.Service
{
    public class ServiceHotelCreateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
    }
}
