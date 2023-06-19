namespace EaglesTMS.DataAccess.Repository.IRepository
{
    public interface ISensorRepository : IRepository<Sensor>
    {
        Task UpdateSensorAsync(Sensor sensorTybe);
        Task DeleteSensorAsync(Sensor sensorTybe);
        Task RestoreSensorAsync(Sensor sensorTybe);
    }
}
