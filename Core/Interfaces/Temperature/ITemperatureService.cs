
using Core.Models;

namespace Core.Interfaces.Temperature
{
    public interface ITemperatureService : IDataTemplateService<TemperatureMesurment>
    {
        TemperatureMesurment GetLatest(string greenhouseId);
    }
}
