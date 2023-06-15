using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EaglesTMS.DataAccess.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private readonly ApplicationDbContext _context;
        public JobRepository(ApplicationDbContext db) : base(db)
        {
        }
        public async Task UpdateAsync(Job job)
        {
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
        }

    }
}
