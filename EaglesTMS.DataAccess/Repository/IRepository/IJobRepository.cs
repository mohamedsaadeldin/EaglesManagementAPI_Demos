using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EaglesTMS.DataAccess.Repository.IRepository
{
    public interface IJobRepository : IRepository<Job>
    {
        Task UpdateAsync(Job job);
    }
}
