namespace EaglesTMS.DataAccess.Repository
{
    public class NationalitiesRepository : Repository<Nationalities> , INationalitiesRepository 
    {
        public NationalitiesRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
