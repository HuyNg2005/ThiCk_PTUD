using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaHang.Data;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Dtos.Room;
using QuanLyNhaHang.Entities;
using QuanLyNhaHang.Repository;

namespace QuanLyNhaHang.Service.Room
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<RoomEntity> _roomRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public RoomService(IRepository<RoomEntity> roomRepository, AppDbContext context, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<RoomDto>> GetAllAsync()
        {
            var rooms = await _roomRepository.GetAllAsync();
            return _mapper.Map<List<RoomDto>>(rooms);
        }

        public async Task<RoomDto> GetByIdAsync(Guid id)
        {
            var room = await _roomRepository.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<RoomDto>(room);
        }

        public async Task<RoomDto> CreateAsync(RoomCreateRequestDto request)
        {
            var room = _mapper.Map<RoomEntity>(request);
            await _roomRepository.CreateAsync(room);
            return _mapper.Map<RoomDto>(room);
        }

        public async Task<RoomDto> UpdateAsync(Guid id, RoomCreateRequestDto request)
        {
            var room = await _roomRepository.FirstOrDefault(x => x.Id == id);
            if (room == null)
                throw new Exception("Room not found");

            _mapper.Map(request, room);

            await _roomRepository.UpdateAsync(room);
            return _mapper.Map<RoomDto>(room);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _roomRepository.DeleteAsync(id);
            return true;
        }

        public async Task<List<RoomDto>> GetRoomsByFilterAsync(RoomEntity.RoomStatus? status, RoomEntity.RoomType? type)
        {
            var query = _roomRepository.AsQueryable();

            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (type.HasValue)
                query = query.Where(x => x.Type == type.Value);

            var rooms = await query.ToListAsync();

            if (rooms.Count == 0)
                throw new Exception("Không tìm thấy phòng nào với bộ lọc đã cho");

            return _mapper.Map<List<RoomDto>>(rooms);
        }

        public async Task<List<RoomDto>> GetRoomsByCustomerNameAsync(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                throw new Exception("Tên khách hàng không được để trống");

            var query = _roomRepository.AsQueryable()
                .Include(r => r.Bookings)
                .ThenInclude(b => b.BookingDetails)
                .ThenInclude(bd => bd.Customer)
                .Where(r => r.Bookings.Any(b => b.CheckOut == null && b.BookingDetails.Any(bd => bd.Customer.FullName.Contains(customerName))));

            var rooms = await query.ToListAsync();

            if (rooms.Count == 0)
                throw new Exception("Không tìm thấy phòng với ten khách hàng");

            return _mapper.Map<List<RoomDto>>(rooms);
        }
        public async Task<PagedResult<RoomDto>> GetPagedAsync(PagingRequest request)
        {
            return await _roomRepository.GetPagedAsync<RoomDto>(request.PageIndex, request.PageSize, _mapper);
        }

    }
}

