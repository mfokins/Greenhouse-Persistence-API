using System.Collections.Generic;
using Core.Interfaces.Greenhouse;
using Core.Interfaces.Humidity;
using Core.Models;

namespace Core.Services
{
    public class HumidityService : IHumidityService
    {
        private readonly IHumidityRepository _humidityRepository;
        private readonly IGreenhouseService _greenhouseService;

        public HumidityService(IHumidityRepository humidityRepository, IGreenhouseService greenhouseService)
        {
            _humidityRepository = humidityRepository;
            _greenhouseService = greenhouseService;
        }
        public void Add(HumidityMeasurement entity)
        {
            if (!_greenhouseService.IsCreated(entity.GreenHouseId))
            {
                _greenhouseService.Create(entity.GreenHouseId);
            }
            _humidityRepository.Add(entity);
        }

        public void Update(HumidityMeasurement entity)
        {
            _humidityRepository.Update(entity);
        }

        public void Delete(HumidityMeasurement entity)
        {
            _humidityRepository.Delete(entity);
        }

        public HumidityMeasurement Get(int id, string greenHouseId)
        {
            return _humidityRepository.Get( id,  greenHouseId);
        }

        public IEnumerable<HumidityMeasurement> GetAll(string greenhouseId, int pageNumber = 0, int pageSize = 25)
        {
            return _greenhouseService.IsCreated(greenhouseId) ? _humidityRepository.GetAll(greenhouseId, pageNumber, pageSize) : null;
        }

        public HumidityMeasurement GetLatest(string greenhouseId)
        {
            return _greenhouseService.IsCreated(greenhouseId) ? _humidityRepository.GetLatest(greenhouseId) : null;
        }
    }
}