using Core.Interfaces.Greenhouse;
using Core.Models;

namespace Core.Services
{
    public class GreenhouseService : IGreenhouseService
    {
        private IGreenhouseRepository _repository;

        public GreenhouseService(IGreenhouseRepository repository)
        {
            _repository = repository;
        }

        public void Create(string id)
        {
            _repository.Add(new Greenhouse() {GreenHouseId = id});
        }

        public Greenhouse Get(string id)
        {
            return _repository.Get(id);
        }

        public bool IsCreated(string id)
        {
            return _repository.Get(id) != null;
        }

        public void UpdateGreenhouse(Greenhouse greenhouse)
        {
            _repository.Update(greenhouse);
        }
    }
}