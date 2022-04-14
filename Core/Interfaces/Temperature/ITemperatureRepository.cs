using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces.Temperature
{
    public interface ITemperatureRepository : IDataReadRepository<TemperatureMeasurement>, IDataWriteRepository<TemperatureMeasurement>
    {
        //Some extra stuff here if needed
        IEnumerable<TemperatureMeasurement> GetAll(string greenhouseId);
        TemperatureMeasurement GetLatest(string greenhouseId);
    }
}
