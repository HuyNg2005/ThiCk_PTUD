using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.Dtos.BookingDetail;
using QuanLyNhaHang.Dtos.BookingDetailDto;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Service.BookingDetail;

namespace QuanLyNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingDetailController : ControllerBase
    {
        private readonly IBookingDetailService _service;

        public BookingDetailController(IBookingDetailService service)
        {
            _service = service;
        }
        

        [HttpGet("guests/{bookingId}")]
        public async Task<IActionResult> GetGuests(Guid bookingId)
        {
            var result = await _service.GetGuestsByBookingAsync(bookingId);
            return Ok(result);
        }

        [HttpGet("count/{bookingId}")]
        public async Task<IActionResult> CountGuests(Guid bookingId)
        {
            var count = await _service.CountGuestsAsync(bookingId);
            return Ok(count);
        }

        [HttpGet("summary/{bookingId}")]
        public async Task<IActionResult> GetSummary(Guid bookingId)
        {
            try
            {
                var result = await _service.GetBookingSummaryAsync(bookingId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] PagingRequest request)
        {
            var result = await _service.GetPagedAsync(request);
            return Ok(result);
        }
        [HttpPost("add-guest")]
        public async Task<IActionResult> AddGuestToBooking([FromBody] BookingAddGuestDto dto)
        {
            try
            {
                await _service.AddGuestToBookingAsync(dto.BookingId, dto.CustomerId);
                return Ok(new
                {
                    Success = true,
                    Message = "Thêm khách hàng vào booking thành công."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
