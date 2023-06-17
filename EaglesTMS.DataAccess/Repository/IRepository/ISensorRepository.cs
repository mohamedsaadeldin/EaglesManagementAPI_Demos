using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EaglesTMS.DataAccess.Repository.IRepository
{
    public interface ISensorRepository : IRepository<Sensor>
    {
        Task UpdateSensor(Sensor sensorTybe);
    }
}
