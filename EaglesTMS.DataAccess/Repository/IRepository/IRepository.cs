

namespace EaglesTMS.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string includeProperties = null);
        Task <IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, string includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string includeProperties = null);
        Task AddAsync(T item);
        Task AddRangeAsync(IEnumerable<T> items);
        Task RemoveAsync(T item);
        Task RemoveRangeAsync(IEnumerable<T> items);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> filter);
        Task SaveAsync();
    }
}
