using QuanLyNhaHang.Dtos.BookingDetail;
using QuanLyNhaHang.Dtos.BookingServiceDetail;
using QuanLyNhaHang.Dtos.BookingServiceDetailDto;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Entities;

namespace QuanLyNhaHang.Service.BookingDetailService
{
    public interface IBookingDetailServiceService
    {
        Task<IEnumerable<BookingDetailServiceDto>> GetAllAsync();
        Task<BookingDetailServiceDto> GetByIdAsync(Guid id);
        Task<BookingDetailServiceDto> CreateAsync(BookingDetailServiceCreateDto dto);
        Task<BookingDetailServiceDto> UpdateAsync(BookingDetailServiceUpdateDto dto );
        Task<bool> DeleteAsync(Guid id);
        //lay danh sach detail theo phong
        Task<PagedResult<BookingDetailServiceDto>> GetPagedAsync(PagingRequest request);
    }
}
