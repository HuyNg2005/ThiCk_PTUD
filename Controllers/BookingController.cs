using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.Dtos.Booking;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Service.Booking;

namespace QuanLyNhaHang.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BookingController : ControllerBase
{
    private readonly IBookingservice _bookingService;

    public BookingController(IBookingservice bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookingCreateRequestDto dto)
    {
        try
        {
            var result = await _bookingService.CreateAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("checkout/{bookingId}")]
    public async Task<IActionResult> CheckOut(Guid bookingId, [FromQuery] Guid checkOutByUserId)
    {
        try
        {
            var result = await _bookingService.CheckOutAsync(bookingId, checkOutByUserId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _bookingService.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _bookingService.GetAllAsync();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _bookingService.DeleteAsync(id);
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
        var result = await _bookingService.GetPagedAsync(request);
        return Ok(result);
    }
}