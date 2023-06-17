using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EaglesTMS.DataAccess.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private readonly ApplicationDbContext _context;
        public JobRepository(ApplicationDbContext db) : base(db)
        {
            _context = db;
        }
        public async Task UpdateAsync(Job job)
        {
            _context.Update(job);
            await _context.SaveChangesAsync();
        }
    }
}
