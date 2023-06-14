namespace EaglesTMS.DataAccess.Repository
{
    public class JobRepository : Repository<Job> , IJobRepository 
    {
        public JobRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
