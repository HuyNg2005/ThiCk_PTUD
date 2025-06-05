using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.Dtos.Orc;
using QuanLyNhaHang.Service.OrcScanner;

namespace QuanLyNhaHang.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class OcrController: Controller
{
    private readonly IWebHostEnvironment _env;
    private readonly OrcScannerService _ocrService;

    public OcrController(IWebHostEnvironment env)
    {
        _env = env;
        _ocrService = new OrcScannerService();
    }

    [HttpPost("scan-cccd")]
    public async Task<IActionResult> ScanCCCD([FromForm] OcrRequest request)
    {
        if (request.Image == null || request.Image.Length == 0)
            return BadRequest("Vui lòng chọn ảnh CCCD.");

        var folder = Path.Combine(_env.WebRootPath ?? _env.ContentRootPath, "uploads");
        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

        var filePath = Path.Combine(folder, Guid.NewGuid() + Path.GetExtension(request.Image.FileName));
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.Image.CopyToAsync(stream);
        }

        var rawText = _ocrService.ExtractTextFromImage(filePath);
        var customer = _ocrService.ExtractCustomerInfo(rawText);

        System.IO.File.Delete(filePath);

        return Ok(new
        {
            rawText,
            data = customer
        });
    }

}