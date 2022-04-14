using Core.Interfaces.Temperature;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class TemperatureService : ITemperatureService
    {
        private readonly ITemperatureRepository _repository;
        public TemperatureService(ITemperatureRepository repository)
        {
            _repository = repository;
        }
        public void Add(TemperatureMeasurement entity)
        {
            _repository.Add(entity);
        }

        public void Delete(TemperatureMeasurement entity)
        {
            _repository.Delete(entity);
        }

        public TemperatureMeasurement Get(int id)
        {
            return _repository.Get(id);
        }


        public IEnumerable<TemperatureMeasurement> GetAll(string greenhouseId)
        {
            return _repository.GetAll(greenhouseId);
        }


        public TemperatureMeasurement GetLatest(string greenhouseId)
        {
            return _repository.GetLatest(greenhouseId);

        }

        public void Update(TemperatureMeasurement entity)
        {
             _repository.Update(entity);
        }
    }
}
