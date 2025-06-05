using static QuanLyNhaHang.Entities.RoomEntity;

namespace QuanLyNhaHang.Dtos.Room
{
    public class RoomDto
    {
        public Guid Id  { get; set; }
        public string Name { get; set; }
        public RoomType Type { get; set; }
        public RoomStatus Status { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
