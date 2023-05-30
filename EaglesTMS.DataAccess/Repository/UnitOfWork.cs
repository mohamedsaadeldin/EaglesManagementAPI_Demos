namespace EaglesTMS.DataAccess.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public INationalitiesRepository Nationalities { get; private set; }

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            Nationalities = new NationalitiesRepository(_db);
        }
    }
}
