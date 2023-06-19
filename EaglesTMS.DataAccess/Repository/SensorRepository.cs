namespace EaglesTMS.DataAccess.Repository
{
    public class SensorRepository : Repository<Sensor>, ISensorRepository
    {
        private readonly ApplicationDbContext _context;
        public SensorRepository(ApplicationDbContext db) : base(db)
        {
            _context = db;
        }

        public async Task DeleteSensorAsync(Sensor sensorTybe)
        {
            sensorTybe.IsDeleted = true;
            _context.Sensors.Update(sensorTybe);
            await _context.SaveChangesAsync();
        }

        public async Task RestoreSensorAsync(Sensor sensorTybe)
        {
            sensorTybe.IsDeleted = false;
            _context.Sensors.Update(sensorTybe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSensorAsync(Sensor sensorTybe)
        {
            _context.Update(sensorTybe);
            await _context.SaveChangesAsync();
        }
    }
}
