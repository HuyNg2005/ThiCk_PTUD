using AutoMapper;
using QuanLyNhaHang.Dtos.BookingServiceDetail;
using QuanLyNhaHang.Dtos.BookingServiceDetailDto;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Entities;
using QuanLyNhaHang.Repository;
using QuanLyNhaHang.Service.BookingDetail;

namespace QuanLyNhaHang.Service.BookingDetailService
{
    public class BookingDetailServiceService : IBookingDetailServiceService
    {
        private readonly IRepository<BookingServiceDetailEntity> _repository;
        private readonly IMapper _mapper;

        public BookingDetailServiceService(IRepository<BookingServiceDetailEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingDetailServiceDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookingDetailServiceDto>>(items);
        }

        public async Task<BookingDetailServiceDto> GetByIdAsync(Guid id)
        {
            var item = await _repository.FirstOrDefault(x => id == x.Id); 
            return _mapper.Map<BookingDetailServiceDto>(item);
        }

        public async Task<BookingDetailServiceDto> CreateAsync(BookingDetailServiceCreateDto dto)
        {
            var entity = _mapper.Map<BookingServiceDetailEntity>(dto);
            entity.Id = Guid.NewGuid();
            await _repository.CreateAsync(entity);
            return _mapper.Map<BookingDetailServiceDto>(entity);
        }
        
        public async Task<BookingDetailServiceDto> UpdateAsync(BookingDetailServiceUpdateDto dto)
        {
            var existing = await _repository.FirstOrDefault(x => x.Id == dto.BookingId);
            if (existing == null)
                throw new Exception("Không tìm thấy dịch vụ");

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);
            return _mapper.Map<BookingDetailServiceDto>(existing);
        }
        
        public async Task<bool> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }
        public async Task<PagedResult<BookingDetailServiceDto>> GetPagedAsync(PagingRequest request)
        {
            return await _repository.GetPagedAsync<BookingDetailServiceDto>(request.PageIndex, request.PageSize, _mapper);
        }

    }
}
