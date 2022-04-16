using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces.Humidity
{
    public interface IHumidityService : IDataTemplateService<HumidityMeasurement>
    {
        HumidityMeasurement GetLatest(string greenhouseId);
    }
}