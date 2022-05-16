using Core.Interfaces.Pot;
using Core.Models;

namespace Core.Services
{
    public class PotService : IPotService
    {
        private readonly IPotRepository _potRepository;

        public PotService(IPotRepository potRepository)
        {
            _potRepository = potRepository;
        }
        public void Add(Pot entity)
        {
            _potRepository.Add(entity);
        }

        public void Delete(Pot entity)
        {
            _potRepository.Delete(entity);
        }

        public Pot Get(int id, string greenHouseId)
        {
            return _potRepository.Get( id,  greenHouseId);
        }

        public IEnumerable<Pot> GetAll(string greenhouseId, int pageNumber = 0, int pageSize = 25)
        {
            return _potRepository.GetAll(greenhouseId, pageNumber, pageSize);
        }

        public void Update(Pot entity)
        {
            _potRepository.Update(entity);
        }
    }
}
