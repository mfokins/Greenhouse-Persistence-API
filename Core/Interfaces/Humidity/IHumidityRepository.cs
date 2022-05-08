using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces.Humidity
{
    public interface IHumidityRepository : IDataReadRepository<HumidityMeasurement>,
        IDataWriteRepository<HumidityMeasurement>
    {
        HumidityMeasurement GetLatest(string greenhouseId);
    }
}