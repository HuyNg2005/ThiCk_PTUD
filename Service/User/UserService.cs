using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaHang.Data;
using QuanLyNhaHang.Dtos.User;
using QuanLyNhaHang.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Repository;

namespace QuanLyNhaHang.Service.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> _rpUserRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<UserEntity> _passwordHasher; //
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration; ///
        public UserService(
            IRepository<UserEntity> rpUser,
            IMapper mapper,
            IPasswordHasher<UserEntity> passwordHasher, //
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration) //
        {
            _rpUserRepository = rpUser;
            _mapper = mapper;
            _passwordHasher = passwordHasher;//
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;//
        }

        public async Task<UserDto> Register(UserRegisterRequest dto)
        {
            var user = _mapper.Map<UserEntity>(dto);

            var existing = await _rpUserRepository.FirstOrDefault(u => u.UserName == dto.UserName);
            if (existing != null)
                throw new Exception("Username already exists.");

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);
            user.CreatedAt = DateTime.UtcNow;
            user.Role = UserEntity.UserRole.Staff;

            var result = await _rpUserRepository.CreateAsync(user);
            return _mapper.Map<UserDto>(result);
        }

        public async Task<string> Login(UserLoginRequest dto)
        {
            var user = await _rpUserRepository.FirstOrDefault(u => u.UserName == dto.UserName);
            if (user == null)
                throw new Exception("Invalid username or password.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new Exception("Invalid username or password.");

            return GenerateToken(user);
        }



        public async Task<UserDto> GetById(Guid id)
        {
            var user = await _rpUserRepository.GetAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Create(UserCreateRequest dto)
        {
            var user = _mapper.Map<UserEntity>(dto);

            var existing = await _rpUserRepository.FirstOrDefault(u => u.UserName == dto.UserName);
            if (existing != null)
                throw new Exception("Username already exists.");
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);         
            var result = await _rpUserRepository.CreateAsync(user);
            return _mapper.Map<UserDto>(result);
        }

        public async Task<bool> Delete(Guid id)
        {
            await _rpUserRepository.DeleteAsync(id);
            return true;
        }
        
        private string GenerateToken (UserEntity user)
        {
            var jwtSetting = _configuration.GetSection("JwtSettings");
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("Role", user.Role.ToString()),
                new Claim("Name", user.FullName.ToString()),
                new Claim("Email", user.Email.ToString()),
                new Claim("Id", user.Id.ToString()),
            };
            var key = new Microsoft.IdentityModel.Tokens
                .SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSetting["Secret"]));
            var creds = new Microsoft.IdentityModel.Tokens
                .SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSetting["Issuer"],
                audience: jwtSetting["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: creds

            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _rpUserRepository.AsQueryable().ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<Guid> Update(UserCreateRequest dto)
        {
            // Lấy user từ DB (đã được tracking)
            var existingUser = await _rpUserRepository.FirstOrDefault(x => x.Id == dto.Id);
            if (existingUser == null)
            {
                throw new Exception("Người dùng không tồn tại.");
            }

            // Map từ dto vào đối tượng đang được tracking
            _mapper.Map(dto, existingUser);

            await _rpUserRepository.UpdateAsync(existingUser);
            return existingUser.Id;
        }
        public async Task<PagedResult<UserDto>> GetPagedAsync(PagingRequest request)
        {
            return await _rpUserRepository.GetPagedAsync<UserDto>(request.PageIndex, request.PageSize, _mapper);
        }

    }
}
