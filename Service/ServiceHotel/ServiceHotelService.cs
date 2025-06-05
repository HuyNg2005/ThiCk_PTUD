using AutoMapper;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Dtos.Service;
using QuanLyNhaHang.Entities;
using QuanLyNhaHang.Repository;

namespace QuanLyNhaHang.Service.ServiceHotel
{
    public class ServiceHotelService : IServiceHotelService
    {
        private readonly IRepository<ServiceHotelEntity> _servicehotelrp;
        private readonly IMapper _mapper;

        public ServiceHotelService(IRepository<ServiceHotelEntity> repository, IMapper mapper)
        {
            _servicehotelrp = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceHotelDto>> GetAllAsync()
        {
            var entities = await _servicehotelrp.GetAllAsync();
            return _mapper.Map<IEnumerable<ServiceHotelDto>>(entities);
        }

        public async Task<ServiceHotelDto> GetByIdAsync(Guid id)
        {
            var entity = await _servicehotelrp.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<ServiceHotelDto>(entity);
        }

        public async Task<ServiceHotelDto> CreateAsync(ServiceHotelCreateDto dto)
        {
            var entity = _mapper.Map<ServiceHotelEntity>(dto);
            await _servicehotelrp.CreateAsync(entity);
            return _mapper.Map<ServiceHotelDto>(entity);
            
        }

        public async Task<ServiceHotelDto> UpdateAsync( ServiceHotelCreateDto dto)
        {
            var existing = await _servicehotelrp.FirstOrDefault( x => x.Id == dto.Id );
            if (existing == null)
                throw new Exception("Service hotel doesn't exist");

            _mapper.Map(dto, existing);
            await _servicehotelrp.UpdateAsync(existing);
            return _mapper.Map<ServiceHotelDto>(existing);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _servicehotelrp.DeleteAsync(id);
            return true;
        }
        public async Task<PagedResult<ServiceHotelDto>> GetPagedAsync(PagingRequest request)
        {
            return await _servicehotelrp.GetPagedAsync<ServiceHotelDto>(request.PageIndex, request.PageSize, _mapper);
        }

    }
}
