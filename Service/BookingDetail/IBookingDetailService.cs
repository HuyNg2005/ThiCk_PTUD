using QuanLyNhaHang.Dtos.BookingDetail;
using QuanLyNhaHang.Dtos.BookingDetailDto;
using QuanLyNhaHang.Dtos.Pagination;

namespace QuanLyNhaHang.Service.BookingDetail
{
    public interface IBookingDetailService
    {
        Task<IEnumerable<BookingDetailDto>> GetGuestsByBookingAsync(Guid bookingId);
        Task<int> CountGuestsAsync(Guid bookingId);
        Task<BookingSummaryDto> GetBookingSummaryAsync(Guid bookingId);
        Task DeleteAsync(Guid id);
        
        Task AddGuestToBookingAsync(Guid bookingId, Guid customerId);
        //lay danh sach detail theo phong 
        Task<PagedResult<BookingDetailDto>> GetPagedAsync(PagingRequest request);
    }
}
