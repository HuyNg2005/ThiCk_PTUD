namespace QuanLyNhaHang.Dtos.Pagination;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalItems { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}