using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Dtos.User;

namespace QuanLyNhaHang.Service.User
{
    public interface IUserService
    {
        Task<UserDto> GetById(Guid id);
        Task<UserDto> Create(UserCreateRequest dto);
        Task<UserDto> Register(UserRegisterRequest dto);
        Task<string> Login(UserLoginRequest dto); // Trả về JWT
        Task<bool> Delete(Guid id);
        Task<List<UserDto>> GetAllAsync();
        Task <Guid>Update( UserCreateRequest dto);
        Task<PagedResult<UserDto>> GetPagedAsync(PagingRequest request);
    }
}
