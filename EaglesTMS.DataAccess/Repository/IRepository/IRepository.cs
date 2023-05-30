

namespace EaglesTMS.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, string includeProperties = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string includeProperties = null);

        void Add(T item);
        void AddRange(IEnumerable<T> items);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> items);
        bool Any();
        bool Any(Expression<Func<T, bool>> filter);
        int Count();
        int Count(Expression<Func<T, bool>> filter);
    }
}
