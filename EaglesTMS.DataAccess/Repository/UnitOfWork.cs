using EaglesTMS.DataAccess.Data;
using EaglesTMS.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
