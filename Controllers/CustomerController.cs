using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.Dtos.Customer;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Service.Customer;

namespace QuanLyNhaHang.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(CustomerCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Update(Guid id, CustomerUpdateDto dto)
    {
        if (id != dto.Id) return BadRequest("Mismatched ID");
        var updated = await _service.UpdateAsync(dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? Ok() : NotFound();
    }

    [HttpGet("idcard/{idCard}")]
    public async Task<IActionResult> GetByIdCard(string idCard)
    {
        var customer = await _service.GetByIdCardAsync(idCard);
        return customer == null ? NotFound() : Ok(customer);
    }
    [HttpGet("filter")]
    public async Task<IActionResult> GetFilter([FromQuery] string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return BadRequest("Text filter cannot be empty");

        var customers = await _service.GetFilter(text);
        return Ok(customers);
    }
    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] PagingRequest request)
    {
        var result = await _service.GetPagedAsync(request);
        return Ok(result);
    }

}