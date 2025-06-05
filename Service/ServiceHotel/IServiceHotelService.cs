using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Dtos.Service;

namespace QuanLyNhaHang.Service.ServiceHotel
{
    public interface IServiceHotelService
    {
        Task<IEnumerable<ServiceHotelDto>> GetAllAsync();
        Task<ServiceHotelDto> GetByIdAsync(Guid id);
        Task<ServiceHotelDto> CreateAsync(ServiceHotelCreateDto dto);
        Task<ServiceHotelDto> UpdateAsync(ServiceHotelCreateDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<PagedResult<ServiceHotelDto>> GetPagedAsync(PagingRequest request);
    }
    
}
