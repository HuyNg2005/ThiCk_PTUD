using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Dtos.User;
using QuanLyNhaHang.Service;
using QuanLyNhaHang.Service.User;

namespace QuanLyNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // private readonly IUserService _service;
        private readonly IUserService _userService; 

        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest dto)
        {
            var result = await _userService.Register(dto);
            return Ok(result);
        }

        // [POST] api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest dto)
        {
            var token = await _userService.Login(dto);
            return Ok(new { token });
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _userService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            try
            {
                var result = await _userService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        
        public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var result = await _userService.Create(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var result = await _userService.Update(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            try
            {
                var result = await _userService.Delete(id);
                return Ok("Xoa thanh cong");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] PagingRequest request)
        {
            var result = await _userService.GetPagedAsync(request);
            return Ok(result);
        }
    }

}
