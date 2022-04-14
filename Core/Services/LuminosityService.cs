using System.Collections.Generic;
using Core.Interfaces.Luminosity;
using Core.Models;

namespace Core.Services
{
    public class LuminosityService : ILuminosityService
    {
        private readonly ILuminosityRepository _repository;

        public LuminosityService(ILuminosityRepository repository)
        {
            _repository = repository;
        }

        public void Add(LuminosityMeasurement entity)
        {
            _repository.Add(entity);
        }

        public void Update(LuminosityMeasurement entity)
        {
            _repository.Update(entity);
        }

        public void Delete(LuminosityMeasurement entity)
        {
            _repository.Delete(entity);
        }

        public LuminosityMeasurement Get(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<LuminosityMeasurement> GetAll(string greenhouseId)
        {
            return _repository.GetAll(greenhouseId);
        }

        public LuminosityMeasurement GetLatest(string greenhouseId)
        {
            return _repository.GetLatest(greenhouseId);
        }
    }
}