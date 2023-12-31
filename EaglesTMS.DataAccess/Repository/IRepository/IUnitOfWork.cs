﻿namespace EaglesTMS.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }
        INationalitiesRepository Nationalities { get; } 
        IJobRepository Jobs { get; }
        ISensorRepository Sensors { get; }
    }
}
