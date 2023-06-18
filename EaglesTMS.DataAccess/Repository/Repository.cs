namespace EaglesTMS.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //_db.Products.Include(c => c.Category).Include(c => c.CoverType);
            dbSet = _db.Set<T>();
        }
        public async Task AddAsync(T item)
        {
           await dbSet.AddAsync(item);
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
            return  await query.ToListAsync();
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
            return await query.ToListAsync();
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
            return await ts.FirstOrDefaultAsync();
            #pragma warning restore CS8603 // Possible null reference return.
        }
        public async Task <bool> AnyAsync()
        {
            return await dbSet.AnyAsync();
        }
        public async Task <bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await dbSet.AnyAsync(filter);
        }

        public async Task RemoveAsync(T item)
        {
            dbSet.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> items)
        {
            dbSet.RemoveRange(items);
            await _db.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> items)
        {
            dbSet.AddRange(items);
            await _db.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await dbSet.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            return await dbSet.CountAsync(filter);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
