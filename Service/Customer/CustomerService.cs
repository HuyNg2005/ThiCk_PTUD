using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaHang.Dtos.Customer;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Entities;
using QuanLyNhaHang.Repository;

namespace QuanLyNhaHang.Service.Customer;

public class CustomerService  : ICustomerService
{
    private readonly IRepository<CustomerEntity> _customerRepository;
    private readonly IMapper _mapper;

    public CustomerService(IRepository<CustomerEntity> repository, IMapper mapper)
    {
        _customerRepository = repository;
        _mapper = mapper;
    }

    public async Task<List<CustomerDto>> GetAllAsync()
    {
        var entities = await _customerRepository.GetAllAsync();
        return _mapper.Map<List<CustomerDto>>(entities);
    }

    public async Task<CustomerDto> GetByIdAsync(Guid id)
    {
        var entity = await _customerRepository.FirstOrDefault( t => t.Id == id);
        return _mapper.Map<CustomerDto>(entity);
    }

    public async Task<CustomerDto> CreateAsync(CustomerCreateDto dto)
    {
        var entity = _mapper.Map<CustomerEntity>(dto);
        await _customerRepository.CreateAsync(entity);
        return _mapper.Map<CustomerDto>(entity);
    }

    public async Task<CustomerDto> UpdateAsync(CustomerUpdateDto dto)
    {
        var entity = await _customerRepository.FirstOrDefault(x => x.Id == dto.Id);
          if (entity == null) 
            throw new Exception("Customer not found");

        _mapper.Map(dto, entity);
        await _customerRepository.UpdateAsync(entity);
        return _mapper.Map<CustomerDto>(entity);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await _customerRepository.DeleteAsync(id);
        return true;
    }

    public async Task<CustomerDto> GetByIdCardAsync(string idCard)
    {
        var entity = await _customerRepository.FirstOrDefault( x => x.IdCard == idCard);
        return _mapper.Map<CustomerDto>(entity);
    }

    public async Task<List<CustomerDto>> GetFilter(string text)
    {
        // tìm danh sach khach hang bao gom khach hang thoa man dieu kien
        // text => co the là CCCD hoac ten khach hang, 

        var custumers = await _customerRepository.AsQueryable().Where(c => c.IdCard.ToLower().Contains(text.ToLower())
                                                                           || c.FullName.ToLower().Contains(text.ToLower())).ToListAsync();
        return _mapper.Map<List<CustomerDto>>(custumers);
    }
    public async Task<PagedResult<CustomerDto>> GetPagedAsync(PagingRequest request)
    {
        return await _customerRepository.GetPagedAsync<CustomerDto>(request.PageIndex, request.PageSize, _mapper);
    }

}