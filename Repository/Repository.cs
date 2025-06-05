using Microsoft.EntityFrameworkCore;
using QuanLyNhaHang.Data;
using System.Linq.Expressions;
using AutoMapper;
using QuanLyNhaHang.Dtos.Pagination;
using QuanLyNhaHang.Dtos.Room;
using QuanLyNhaHang.Entities;
using QuanLyNhaHang.Service.BookingDetail;

namespace QuanLyNhaHang.Repository
{
    public class Repository <TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;
private readonly IBookingDetailService  _bookingDetailService;
    public Repository(AppDbContext context )
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
        _bookingDetailService = _context.BookingDetailservice;
    }

    public IQueryable<TEntity> AsQueryable()
    {
       return _dbSet.AsQueryable();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var result =await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task CreateListAsync(List<TEntity> entity)
    {
        await _dbSet.AddRangeAsync(entity);
        await _context.SaveChangesAsync();
        
    }
    public async Task DeleteAsync(Guid id)
    {
       var result = await _dbSet.FirstOrDefaultAsync(x => EF.Property<Guid>(x, "Id")== id);
        if (result == null)
        {
            throw new Exception("không tìm thấy đối tượng");
        }
        _dbSet.Remove(result);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteListAsync(List<Guid> ids)
    {
        var result = await _dbSet.Where(x=>ids.Contains(EF.Property<Guid>(x, "Id"))).ToListAsync();
        if (result == null)
        {
            throw new Exception("không tìm thấy đối tượng");
        }
        _dbSet.RemoveRange(result);
        await _context.SaveChangesAsync();
    }



    public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var result = await _dbSet.Where(predicate).ToListAsync();
        if (result == null)
        {
            throw new Exception("khong tìm thấy đối tượng");
        }
        return result;
    }

    public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
    {
        var result = await _dbSet.FirstOrDefaultAsync(predicate);
  
        return result;
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        var result = await _dbSet.ToListAsync();
        if (result.Count == 0)
        {
            throw new Exception("Không có dữ liệu");
        }
        return result;
    }

    
    public async Task<TEntity> GetAsync(Guid Id)
    {
        var result = await _dbSet.FirstOrDefaultAsync(x => EF.Property<Guid>(x, "Id")== Id);
        if (result == null)
        {
            throw new Exception("khong tìm thấy đối tượng");
        }
        return result;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var result =_dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
    public async Task<PagedResult<TDto>> GetPagedAsync<TDto>(
        int pageIndex, 
        int pageSize, 
        IMapper mapper)
    {
        var query = _dbSet.AsNoTracking();

        var totalItems = await query.CountAsync();

        var entities = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var items = mapper.Map<List<TDto>>(entities);

        return new PagedResult<TDto>
        {
            Items = items,
            TotalItems = totalItems,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (filter != null)
            query = query.Where(filter);

        foreach (var includeProperty in includeProperties.Split(
                     new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty.Trim());
        }

        return await query.ToListAsync();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();
        if (filter != null)
            query = query.Where(filter);
        return await query.CountAsync();
    }

    public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
    {
        IQueryable<TEntity> query = _context.Set<TEntity>();

        if (filter != null)
            query = query.Where(filter);

        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split
                         (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbSet.AnyAsync(filter);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }
    //
}
    
    
}
