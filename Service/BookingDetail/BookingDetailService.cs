using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaHang.Dtos.BookingDetail;
using QuanLyNhaHang.Dtos.BookingDetailDto;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Entities;
using QuanLyNhaHang.Repository;

namespace QuanLyNhaHang.Service.BookingDetail
{
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IRepository<BookingDetailEntity> _bookingDetailRepo;
        private readonly IRepository<BookingEntity> _bookingRepo;
    private readonly IRepository<CustomerEntity> _customerRepo;
    private readonly IMapper _mapper;

    public BookingDetailService(
        IRepository<BookingDetailEntity> bookingDetailRepo,
        IRepository<BookingEntity> bookingRepo,
        IRepository<CustomerEntity> customerRepo,
        IMapper mapper)
    {
        _bookingDetailRepo = bookingDetailRepo;
        _bookingRepo = bookingRepo;
        _customerRepo = customerRepo;
        _mapper = mapper;
    }

   
    public async Task AddGuestToBookingAsync(Guid bookingId, Guid customerId)
    {
        var booking = await _bookingRepo.FirstOrDefault(
                          x => x.Id == bookingId,
                          includeProperties: "Room")
                      ?? throw new Exception("Booking không tồn tại.");

        var customer = await _customerRepo.FirstOrDefault(x => x.Id == customerId)
                       ?? throw new Exception("Khách hàng không tồn tại.");

        int currentGuestCount = await _bookingDetailRepo.CountAsync(x => x.BookingId == bookingId);
        int maxGuests = GetMaxGuestsByRoomType(booking.Room.Type);

        if (currentGuestCount >= maxGuests)
            throw new Exception($"Phòng chỉ được tối đa {maxGuests} khách.");

        var alreadyExists = await _bookingDetailRepo.AnyAsync(x =>
            x.BookingId == bookingId && x.CustomerId == customerId);
        if (alreadyExists)
            throw new Exception("Khách hàng đã có trong booking này.");

        var newBookingDetail = new BookingDetailEntity
        {
            Id = Guid.NewGuid(),
            BookingId = bookingId,
            CustomerId = customerId
        };

        await _bookingDetailRepo.AddAsync(newBookingDetail);
    }

    private int GetMaxGuestsByRoomType(RoomEntity.RoomType roomType) => roomType switch
    {
        RoomEntity.RoomType.Single => 1,
        RoomEntity.RoomType.Double => 2,
        RoomEntity.RoomType.Standard => 3,
        RoomEntity.RoomType.VIP => 4,
        _ => 2
    };
    
    public async Task<IEnumerable<BookingDetailDto>> GetGuestsByBookingAsync(Guid bookingId)
    {
        var list = await _bookingDetailRepo.GetAllAsync(
            x => x.BookingId == bookingId,
            includeProperties: "Customer");

        return list.Select(x => new BookingDetailDto
        {
            Id = x.Id,
            BookingId = x.BookingId,
            CustomerId = x.CustomerId,
            CustomerFullName = x.Customer?.FullName
        });
    }

    public async Task<int> CountGuestsAsync(Guid bookingId)
    {
        var count = await _bookingDetailRepo.CountAsync(x => x.BookingId == bookingId);
        return count;
    }

    public async Task<BookingSummaryDto> GetBookingSummaryAsync(Guid bookingId)
    {
        // Lấy thông tin booking + phòng
        var booking = await _bookingRepo.FirstOrDefault(
                          x => x.Id == bookingId,
                          includeProperties: "Room,BookingServices")
                      ?? throw new Exception("Booking không tồn tại");

        if (booking.Room == null)
            throw new Exception("Booking chưa gán phòng");

        // Tính số đêm
        var checkOut = booking.CheckOut ?? DateTime.UtcNow;
        int totalNights = (checkOut.Date - booking.CheckIn.Date).Days;
        if (totalNights <= 0) totalNights = 1;

        decimal roomTotal = booking.Room.Price * totalNights;
        decimal serviceTotal = booking.BookingServices?.Sum(s => s.TotalPrice) ?? 0;

        decimal total = roomTotal + serviceTotal;

        return new BookingSummaryDto
        {
            BookingId = booking.Id,
            RoomName = booking.Room.Name,
            RoomPricePerNight = booking.Room.Price,
            TotalNights = totalNights,
            RoomTotalPrice = roomTotal,
            ServiceTotalPrice = serviceTotal,
            TotalPrice = total
        };
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _bookingDetailRepo.FirstOrDefault(x => x.Id == id)
            ?? throw new Exception("Không tìm thấy khách trong booking");
        await _bookingDetailRepo.DeleteAsync(id);
    }


        public async Task<PagedResult<BookingDetailDto>> GetPagedAsync(PagingRequest request)
        {
            return await _bookingDetailRepo.GetPagedAsync<BookingDetailDto>(request.PageIndex, request.PageSize, _mapper);
        }

    }
}
