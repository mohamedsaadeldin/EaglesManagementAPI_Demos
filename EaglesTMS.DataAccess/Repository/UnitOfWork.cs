﻿namespace EaglesTMS.DataAccess.Repository
{
    public class UnitOfWork :IUnitOfWork
    {
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public INationalitiesRepository Nationalities { get; private set; }
        public IJobRepository Jobs { get; private set; }
        public ISensorRepository Sensors { get; private set; }

        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ApplicationUser = new ApplicationUserRepository(_db);
            Nationalities = new NationalitiesRepository(_db);
            Jobs = new JobRepository(_db);
            Sensors = new SensorRepository(_db);
        }
    }
}
