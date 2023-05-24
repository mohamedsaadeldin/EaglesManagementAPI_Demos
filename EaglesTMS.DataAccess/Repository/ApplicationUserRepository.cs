using EaglesTMS.DataAccess.Data;
using EaglesTMS.DataAccess.Repository.IRepository;
using EaglesTMS.Models;

namespace EaglesTMS.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
