using Core.Models;

namespace Core.Interfaces.Temperature
{
    public interface ITemperatureRepository : IDataReadRepository<TemperatureMesurment>, IDataWriteRepository<TemperatureMesurment>
    {
        //Some extra stuff here if needed
        IEnumerable<TemperatureMesurment> GetAll(string greenhouseId);
        TemperatureMesurment GetLatest(string greenhouseId);
    }
}
