using System.Collections.Generic;
using Core.Interfaces.Humidity;
using Core.Models;

namespace Core.Services
{
    public class HumidityService:IHumidityService
    {
        private readonly IHumidityRepository _repository;

        public HumidityService(IHumidityRepository repository)
        {
            _repository = repository;
        }
        public void Add(HumidityMeasurement entity)
        {
            _repository.Add(entity);
        }

        public void Update(HumidityMeasurement entity)
        {
            _repository.Update(entity);
        }

        public void Delete(HumidityMeasurement entity)
        {
            _repository.Delete(entity);
        }

        public HumidityMeasurement Get(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<HumidityMeasurement> GetAll(string greenhouseId)
        {
            return _repository.GetAll(greenhouseId);
        }

        public HumidityMeasurement GetLatest(string greenhouseId)
        {
            return _repository.GetLatest(greenhouseId);
        }
    }
}