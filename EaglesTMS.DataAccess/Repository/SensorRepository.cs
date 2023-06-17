using EaglesTMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaglesTMS.DataAccess.Repository
{
    public class SensorRepository : Repository<Sensor>, ISensorRepository
    {
        private readonly ApplicationDbContext _context;
        public SensorRepository(ApplicationDbContext db) : base(db)
        {
            _context = db;
        }

        public async Task UpdateSensor(Sensor sensorTybe)
        {
            _context.Update(sensorTybe);
            await _context.SaveChangesAsync();
        }
    }
}
