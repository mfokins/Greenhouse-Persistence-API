using Core.Interfaces;
using Core.Interfaces.DioxideCarbon;
using Core.Interfaces.Greenhouse;
using Core.Models;

namespace Core.Services
{
    public class DioxideCarbonService : IDataTemplateService<DioxideCarbonMeasurement>
    {
        private readonly IDioxideCarbonRepository _dioxiderepository;
        private readonly IGreenhouseService _greenhouseService;

        public DioxideCarbonService(IDioxideCarbonRepository dioxiderepository, IGreenhouseService greenhouseService)
        {
            _dioxiderepository = dioxiderepository;
            _greenhouseService = greenhouseService;
        }

        public void Add(DioxideCarbonMeasurement entity)
        {
            if (!_greenhouseService.IsCreated(entity.GreenHouseId))
            {
                _greenhouseService.Create(entity.GreenHouseId);
            }
            _dioxiderepository.Add(entity);
        }

        public void Delete(DioxideCarbonMeasurement entity)
        {
            _dioxiderepository.Delete(entity);
        }

        public DioxideCarbonMeasurement Get(int id, string greenHouseId)
        {
            return _dioxiderepository.Get( id,  greenHouseId);
        }


        public IEnumerable<DioxideCarbonMeasurement> GetAll(string greenhouseId, int pageNumber = 0, int pageSize = 25)
        {
            return _greenhouseService.IsCreated(greenhouseId) ? _dioxiderepository.GetAll(greenhouseId, pageNumber, pageSize) : null;
        }


        public DioxideCarbonMeasurement GetLatest(string greenhouseId)
        {
            return _greenhouseService.IsCreated(greenhouseId) ? _dioxiderepository.GetLatest(greenhouseId) : null;
        }

        public void Update(DioxideCarbonMeasurement entity)
        {
            _dioxiderepository.Update(entity);
        }
    }
}