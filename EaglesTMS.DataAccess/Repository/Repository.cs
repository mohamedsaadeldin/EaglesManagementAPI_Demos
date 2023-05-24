using EaglesTMS.DataAccess.Data;
using EaglesTMS.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EaglesTMS.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //_db.Products.Include(c=>c.Category).Include(c=>c.CoverType)
            dbSet = _db.Set<T>();
        }
        public void Add(T item)
        {
            dbSet.Add(item);
        }


        public virtual IEnumerable<T> GetAll(string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            IQueryable<T> query = dbSet.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            IQueryable<T> ts = dbSet.AsQueryable();
            ts = ts.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    ts = ts.Include(includeProp);
                }
            }

#pragma warning disable CS8603 // Possible null reference return.
            return ts.FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }
        public bool Any()
        {
            return dbSet.Any();
        }
        public bool Any(Expression<Func<T, bool>> filter)
        {
            return dbSet.Any(filter);
        }

        public void Remove(T item)
        {
            dbSet.Remove(item);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            dbSet.RemoveRange(items);
        }

        public void AddRange(IEnumerable<T> items)
        {
            dbSet.AddRange(items);
        }

        public int Count()
        {
            return dbSet.Count();
        }

        public int Count(Expression<Func<T, bool>> filter)
        {
            return dbSet.Count(filter);
        }
    }
}
