using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Dtos.Room;
using QuanLyNhaHang.Entities;
using QuanLyNhaHang.Service.Room;

namespace QuanLyNhaHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _roomService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _roomService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] RoomCreateRequestDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _roomService.CreateAsync(request);
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RoomCreateRequestDto request)
        {
            var result = await _roomService.UpdateAsync(id, request);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _roomService.DeleteAsync(id);
            if (!success) return NotFound();
            return Ok();
        }
        // Lấy phòng theo bộ lọc trạng thái và loại phòng
        // ví dụ: GET api/room/filter?status=Available&type=VIP
        [HttpGet("filter")]
        public async Task<IActionResult> GetRoomsByFilter([FromQuery] RoomEntity.RoomStatus? status, [FromQuery] RoomEntity.RoomType? type)
        {
            var rooms = await _roomService.GetRoomsByFilterAsync(status, type);
            return Ok(rooms);
        }

        // Tìm phòng theo tên khách hàng đang ở
        // ví dụ: GET api/room/search?customerName=Nguyen
        [HttpGet("search")]
        public async Task<IActionResult> GetRoomsByCustomerName([FromQuery] string customerName)
        {
            if (string.IsNullOrEmpty(customerName))
                return BadRequest("Customer name is required");

            var rooms = await _roomService.GetRoomsByCustomerNameAsync(customerName);
            return Ok(rooms);
        }
        
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] PagingRequest request)
        {
            var result = await _roomService.GetPagedAsync(request);
            return Ok(result);
        }

    }
}
