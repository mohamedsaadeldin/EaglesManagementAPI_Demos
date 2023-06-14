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
        public async Task AddAsync(T item)
        {
            dbSet.Add(item);
        }

        public virtual async Task <IEnumerable<T>> GetAllAsync(string includeProperties = null)
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

        public virtual async Task <IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, string includeProperties = null)
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
        public async Task <T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string includeProperties = null)
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
        public async Task <bool> AnyAsync()
        {
            return dbSet.Any();
        }
        public async Task <bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return dbSet.Any(filter);
        }

        public async Task RemoveAsync(T item)
        {
            dbSet.Remove(item);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> items)
        {
            dbSet.RemoveRange(items);
        }

        public async Task AddRangeAsync(IEnumerable<T> items)
        {
            dbSet.AddRange(items);
        }

        public async Task<int> CountAsync()
        {
            return dbSet.Count();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            return dbSet.Count(filter);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
