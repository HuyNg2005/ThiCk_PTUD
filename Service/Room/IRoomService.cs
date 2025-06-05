using AutoMapper;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Dtos.Room;
using QuanLyNhaHang.Entities;

namespace QuanLyNhaHang.Service.Room
{
    public interface IRoomService
    {
        Task<List<RoomDto>> GetAllAsync();
        Task<RoomDto> GetByIdAsync(Guid id);
        Task<RoomDto> CreateAsync(RoomCreateRequestDto request);
        Task<RoomDto> UpdateAsync(Guid id, RoomCreateRequestDto request);
        Task<bool> DeleteAsync(Guid id);
        //lay dnah sach phong theo bo loc ..theo ttrang thai.theo loai phong
        //tim phong theo ten khach hang dang o
        Task<List<RoomDto>> GetRoomsByFilterAsync(RoomEntity.RoomStatus? status, RoomEntity.RoomType? type);
        Task<List<RoomDto>> GetRoomsByCustomerNameAsync(string customerName);
        Task<PagedResult<RoomDto>> GetPagedAsync(PagingRequest request);
    }  
}
