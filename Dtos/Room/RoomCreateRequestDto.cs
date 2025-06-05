using static QuanLyNhaHang.Entities.RoomEntity;

namespace QuanLyNhaHang.Dtos.Room
{
    public class RoomCreateRequestDto
    {
        public Guid RoomId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
