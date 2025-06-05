using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.Dtos.BookingDetail;
using QuanLyNhaHang.Dtos.BookingServiceDetail;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Service.BookingDetailService;

namespace QuanLyNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingDetailServcieController : ControllerBase
    {
        private readonly IBookingDetailServiceService _service;

        public BookingDetailServcieController(IBookingDetailServiceService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BookingDetailServiceCreateDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.ServiceId}, result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BookingDetailServiceUpdateDto request)
        {
            var result = await _service.UpdateAsync(request);
            if (result == null) return NotFound();
            return Ok(result);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] PagingRequest request)
        {
            var result = await _service.GetPagedAsync(request);
            return Ok(result);
        }

    }
}