using QuanLyNhaHang.Dtos.Booking;
using QuanLyNhaHang.Dtos.Pagination;

namespace QuanLyNhaHang.Service.Booking;

public interface IBookingservice
{
    Task<BookingDto> CreateAsync(BookingCreateRequestDto dto);
    Task<BookingDto> CheckOutAsync(Guid bookingId, Guid checkOutByUserId);
    Task<BookingDto> GetByIdAsync(Guid id);
    Task<IEnumerable<BookingDto>> GetAllAsync();
    Task DeleteAsync(Guid id);
    Task<PagedResult<BookingDto>> GetPagedAsync(PagingRequest request);
    //
    
    //Create => lay theo phong => chỉ lấy nhung booking chua co check out 
    // => Ktra phong room = Id == roomid && status == Available => bao loi
    // => Tạo BookingEntity => CreatedAt RoomId CheckInByUserId Id CheckIn => booking
    
    
    // UPdate => Map List Customers > List BookingDetailEntity khi khai bao autoMapper thi bo qua BookingId > Map tay BookingId = booking.Id vao => bookingDetails.
    // => Map List Services > BookingServiceEntity khi khai bao autoMapper thi bo qua BookingId > Map tay BookingId = booking.Id vao => bookingServices.
    // booking.BookingDetails = bookingDetails
    // booking.BookingServices = bookingServices
    // tạo booking.
    
}