namespace QuanLyNhaHang.Dtos.Room;

public class RoomUpdateRequest
{
    public Guid RoomId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}