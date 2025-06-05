using QuanLyNhaHang.Dtos.Customer;
using QuanLyNhaHang.Dtos.Pagination;

namespace QuanLyNhaHang.Service.Customer;

public interface ICustomerService
{
    Task<List<CustomerDto>> GetAllAsync();
    Task<CustomerDto> GetByIdAsync(Guid id);
    Task<CustomerDto> CreateAsync(CustomerCreateDto dto);
    Task<CustomerDto> UpdateAsync(CustomerUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<CustomerDto> GetByIdCardAsync(string idCard); // OCR use //lay danh sach theo ten lkasasch hang

    // Controller ???
    Task<List<CustomerDto>> GetFilter(string text);
    
    Task<PagedResult<CustomerDto>> GetPagedAsync(PagingRequest request);
}