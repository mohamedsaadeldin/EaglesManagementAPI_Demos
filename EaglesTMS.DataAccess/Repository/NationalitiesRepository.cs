namespace EaglesTMS.DataAccess.Repository
{
    public class NationalitiesRepository : Repository<Nationalities> , INationalitiesRepository 
    {
        private readonly ApplicationDbContext _context;
        public NationalitiesRepository(ApplicationDbContext db) : base(db)
        {
            _context = db;
        }

        public async Task UpdateNationalityAsync(Nationalities nationalities)
        {
            _context.Update(nationalities);
            await _context.SaveChangesAsync();
        }
    }
}
