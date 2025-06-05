using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaHang.Data;
using QuanLyNhaHang.Dtos.Booking;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Entities;
using QuanLyNhaHang.Repository;

namespace QuanLyNhaHang.Service.Booking;

public class BookingService : IBookingservice
{
    private readonly IRepository<BookingEntity> _bookingRepository;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public BookingService(AppDbContext context, IMapper mapper,  IRepository<BookingEntity> bookingRepository)
    {
        _context = context;
        _mapper = mapper;
        _bookingRepository = bookingRepository;
    }

    public async Task<BookingDto> CreateAsync(BookingCreateRequestDto dto)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == dto.RoomId);
        if (room == null || room.Status != RoomEntity.RoomStatus.Available)
            throw new Exception("Phòng không tồn tại hoặc đang không khả dụng.");

        var booking = _mapper.Map<BookingEntity>(dto);
        booking.Id = Guid.NewGuid();
        booking.CreatedAt = DateTime.UtcNow;

        // Gán trạng thái phòng sang "Booked"
        room.Status = RoomEntity.RoomStatus.Booked;

        await _context.Bookings.AddAsync(booking);
        await _context.SaveChangesAsync();

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<BookingDto> CheckOutAsync(Guid bookingId, Guid checkOutByUserId)
    {
        var booking = await _context.Bookings
            .Include(b => b.Room)
            .FirstOrDefaultAsync(b => b.Id == bookingId);

        if (booking == null)
            throw new Exception("Không tìm thấy booking.");

        if (booking.CheckOut != null)
            throw new Exception("Booking đã được check-out.");

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == checkOutByUserId);
        if (user == null)
            throw new Exception("Người dùng không tồn tại.");

        booking.CheckOut = DateTime.UtcNow;
        booking.CheckOutByUserId = checkOutByUserId;

        // Trả phòng
        booking.Room.Status = RoomEntity.RoomStatus.Available;

        // Tính tổng tiền: số đêm x giá phòng
        var stayNights = (booking.CheckOut.Value - booking.CheckIn).Days;
        stayNights = stayNights <= 0 ? 1 : stayNights;
        var roomCost = stayNights * booking.Room.Price;

        var serviceCost = await _context.BookingServiceDetails
            .Where(s => s.BookingId == bookingId)
            .SumAsync(s => s.TotalPrice);

        booking.TotalPrice = roomCost + serviceCost;

        await _context.SaveChangesAsync();

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<BookingDto> GetByIdAsync(Guid id)
    {
        var booking = await _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.BookingDetails).ThenInclude(d => d.Customer)
            .Include(b => b.BookingServices).ThenInclude(s => s.Service)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (booking == null)
            throw new Exception("Không tìm thấy booking.");

        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<IEnumerable<BookingDto>> GetAllAsync()
    {
        var bookings = await _context.Bookings
            .Include(b => b.Room)
            .Include(b => b.CheckInByUser)
            .Include(b => b.CheckOutByUser)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BookingDto>>(bookings);
    }

    public async Task DeleteAsync(Guid id)
    {
        var booking = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);
        if (booking == null)
            throw new Exception("Không tìm thấy booking.");

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
    }

    public async Task<PagedResult<BookingDto>> GetPagedAsync(PagingRequest request)
    {
        return await _bookingRepository.GetPagedAsync<BookingDto>(request.PageIndex, request.PageSize, _mapper);
    }

}