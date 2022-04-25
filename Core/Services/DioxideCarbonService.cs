using System.Collections.Generic;
using Core.Interfaces.DioxideCarbon;
using Core.Models;

namespace Core.Services
{

    public class DioxideCarbonService : IDioxideCarbonRepository
    {
        private readonly IDioxideCarbonRepository _repository;

        public DioxideCarbonService(IDioxideCarbonRepository repository)
        {
            _repository = repository;
        }

        public void Add(DioxideCarbonMeasurement entity)
        {
            _repository.Add(entity);
        }

        public void Delete(DioxideCarbonMeasurement entity)
        {
            _repository.Delete(entity);
        }

        public DioxideCarbonMeasurement Get(int id)
        {
            return _repository.Get(id);
        }


        public IEnumerable<DioxideCarbonMeasurement> GetAll(string greenhouseId)
        {
            return _repository.GetAll(greenhouseId);
        }


        public DioxideCarbonMeasurement GetLatest(string greenhouseId)
        {
            return _repository.GetLatest(greenhouseId);
        }

        public void Update(DioxideCarbonMeasurement entity)
        {
            _repository.Update(entity);
        }
    }
}