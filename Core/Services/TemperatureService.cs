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
        public void Add(TemperatureMesurment entity)
        {
            _repository.Add(entity);
        }

        public void Delete(TemperatureMesurment entity)
        {
            _repository.Delete(entity);
        }

        public TemperatureMesurment Get(int id)
        {
            return _repository.Get(id);
        }


        public IEnumerable<TemperatureMesurment> GetAll(string greenhouseId)
        {
            return _repository.GetAll(greenhouseId);
        }


        public TemperatureMesurment GetLatest(string greenhouseId)
        {
            return _repository.GetLatest(greenhouseId);

        }

        public void Update(TemperatureMesurment entity)
        {
             _repository.Update(entity);
        }
    }
}
