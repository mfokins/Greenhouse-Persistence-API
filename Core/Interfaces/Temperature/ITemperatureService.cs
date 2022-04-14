
using Core.Models;

namespace Core.Interfaces.Temperature
{
    public interface ITemperatureService : IDataTemplateService<TemperatureMeasurement>
    {
        TemperatureMeasurement GetLatest(string greenhouseId);
    }
}
