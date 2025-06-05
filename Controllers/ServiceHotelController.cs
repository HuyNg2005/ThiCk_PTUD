using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Dtos.Service;
using QuanLyNhaHang.Service.ServiceHotel;

namespace QuanLyNhaHang.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceHotelController : ControllerBase
{
    private readonly IServiceHotelService _serviceHotel;

    public ServiceHotelController(IServiceHotelService service)
    {
        _serviceHotel = service;
    }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _serviceHotel.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _serviceHotel.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ServiceHotelCreateDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _serviceHotel.CreateAsync(request);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ServiceHotelCreateDto request)
        {
            var result = await _serviceHotel.UpdateAsync(request);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _serviceHotel.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok();
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] PagingRequest request)
        {
            var result = await _serviceHotel.GetPagedAsync(request);
            return Ok(result);
        }

    }
