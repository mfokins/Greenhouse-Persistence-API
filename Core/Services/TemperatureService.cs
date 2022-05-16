using Core.Interfaces.Greenhouse;
using Core.Interfaces.Temperature;
using Core.Models;

namespace Core.Services
{
    public class TemperatureService : ITemperatureService
    {
        private readonly ITemperatureRepository _temperatureRepository;
        private readonly IGreenhouseService _greenhouseService;

        public TemperatureService(ITemperatureRepository temperatureRepository, IGreenhouseService greenhouseService)
        {
            _temperatureRepository = temperatureRepository;
            _greenhouseService = greenhouseService;
        }
        public void Add(TemperatureMeasurement entity)
        {
            if (!_greenhouseService.IsCreated(entity.GreenHouseId))
            {
                _greenhouseService.Create(entity.GreenHouseId);
            }
            _temperatureRepository.Add(entity);

        }

        public void Delete(TemperatureMeasurement entity)
        {
            _temperatureRepository.Delete(entity);
        }

        public TemperatureMeasurement Get(int id, string greenHouseId)
        {
            return _temperatureRepository.Get( id,  greenHouseId);
        }


        public IEnumerable<TemperatureMeasurement> GetAll(string greenhouseId, int pageNumber = 0, int pageSize = 25)
        {
            return _greenhouseService.IsCreated(greenhouseId) ? _temperatureRepository.GetAll(greenhouseId, pageNumber, pageSize) : null;
        }


        public TemperatureMeasurement GetLatest(string greenhouseId)
        {
            return _greenhouseService.IsCreated(greenhouseId) ? _temperatureRepository.GetLatest(greenhouseId) : null;
        }

        public void Update(TemperatureMeasurement entity)
        {
            _temperatureRepository.Update(entity);
        }
    }
}
