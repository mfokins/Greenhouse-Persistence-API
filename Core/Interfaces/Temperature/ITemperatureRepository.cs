using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces.Temperature
{
    public interface ITemperatureRepository : IDataReadRepository<TemperatureMeasurement>, IDataWriteRepository<TemperatureMeasurement>
    {
        TemperatureMeasurement GetLatest(string greenhouseId);
    }
}
