using EaglesTMS.DataAccess.Data;
using EaglesTMS.DataAccess.Repository.IRepository;
using EaglesTMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaglesTMS.DataAccess.Repository
{
    public class NationalitiesRepository : Repository<Nationalities> , INationalitiesRepository 
    {
        public NationalitiesRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
