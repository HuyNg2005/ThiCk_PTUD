using System.Text.RegularExpressions;
using QuanLyNhaHang.Dtos.Customer;
using Tesseract;

namespace QuanLyNhaHang.Service.OrcScanner;

public class OrcScannerService
{
    private readonly string _tessdataPath = @"C:\Program Files\Tesseract-OCR\tessdata"; // hoặc nơi bạn cài
    private readonly string _lang = "vie+eng";

    public string ExtractTextFromImage(string imagePath)
    {
        try
        {
            using var engine = new TesseractEngine(_tessdataPath, _lang, EngineMode.Default);
            using var img = Pix.LoadFromFile(imagePath);
            using var page = engine.Process(img);
            return page.GetText();
        }
        catch (Exception ex)
        {
            throw new Exception($"Không thể khởi tạo Tesseract: {ex.Message}. Đường dẫn tessdata: {_tessdataPath}");
        }
    }

    public CustomerFromOcrDto ExtractCustomerInfo(string text)
    {
        var result = new CustomerFromOcrDto();
        var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Trim())
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToList();

        foreach (var line in lines)
        {
            if (result.IdCard == null && Regex.IsMatch(line, @"\b\d{9,12}\b"))
                result.IdCard = Regex.Match(line, @"\b\d{9,12}\b").Value;

            if (result.FullName == null && Regex.IsMatch(line, @"^[A-Z\sÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠƯẠ-ỹ]{5,}$"))
                result.FullName = line;

            if (result.DateOfBirth == null && Regex.IsMatch(line, @"\b\d{1,2}/\d{1,2}/\d{4}\b"))
                result.DateOfBirth = Regex.Match(line, @"\b\d{1,2}/\d{1,2}/\d{4}\b").Value;
        }

        return result;
    }

}
