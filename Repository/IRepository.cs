using QuanLyNhaHang.Entities;
using System.Linq.Expressions;
using AutoMapper;
using QuanLyNhaHang.Dtos.Pagination;

namespace QuanLyNhaHang.Repository
{
    public interface IRepository <TEntity> where TEntity : class
    {
        IQueryable<TEntity> AsQueryable (); //hanh dong truy van, gom nhieu thao tac, tu truy van
        // tắt sử dụng thành công
        // create: hành động tạo đối tượng
        Task<TEntity> CreateAsync (TEntity entity);

        Task CreateListAsync(List<TEntity> entity);

        //update: hành động cập nhật đối tượng
        Task<TEntity> UpdateAsync (TEntity entity);

        //delete: hành động xóa nhiều đối tượng
        Task DeleteListAsync(List<Guid>id);

        //delete: hành động xóa 1 đối tượng
        Task DeleteAsync (Guid id);

        //Get: GetALL
        //Get: lấy 1 đối tượng theo Id\
        Task<TEntity> GetAsync(Guid Id);

        //GetAll: lấy tất cả dữ liệu thuộc đối tượng T
        Task<List<TEntity>> GetAllAsync ();

        //lấy đối tượng theo điều kiện
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        //lấy danh sách đối tượng theo điều kiện
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<PagedResult<TDto>> GetPagedAsync<TDto>(int pageIndex, int pageSize, IMapper mapper);
        
        //
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "");
        
        //
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        //
        Task<TEntity> FirstOrDefault(
            Expression<Func<TEntity, bool>> filter,
            string includeProperties = ""
        );
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
        
        Task AddAsync(TEntity entity);
    }
}