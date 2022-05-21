using Core.Interfaces;
using Core.Interfaces.Greenhouse;
using Core.Models;

namespace Core.Services;

public class MoistureService : IMoistureService
{
    private readonly IMoistureRepository _moistureRepository;

    private readonly IGreenhouseService _greenhouseService;


    public MoistureService(IMoistureRepository moistureRepository, IGreenhouseService greenhouseService)
    {
        this._greenhouseService = greenhouseService;
        this._moistureRepository = moistureRepository;
    }

    public void Add(MoistureMeasurement entity)
    {
        if (!_greenhouseService.IsCreated(entity.GreenHouseId))
        {
            _greenhouseService.Create(entity.GreenHouseId);
        }

        _moistureRepository.Add(entity);
    }

    public void Update(MoistureMeasurement entity)
    {
        _moistureRepository.Update(entity);
    }

    public void Delete(MoistureMeasurement entity)
    {
        _moistureRepository.Delete(entity);
    }

    public MoistureMeasurement Get(int id, string greenHouseId)
    {
        return _moistureRepository.Get(id, greenHouseId);
    }
    
    public IEnumerable<MoistureMeasurement> GetAll(string greenhouseId, int potId, int pageNumber = 0,
        int pageSize = 25)
    {
        return _greenhouseService.IsCreated(greenhouseId)
            ? _moistureRepository.GetAll(greenhouseId, potId, pageNumber, pageSize)
            : null;
    }

    public MoistureMeasurement GetLatest(string greenhouseId, int potId)
    {
        return _greenhouseService.IsCreated(greenhouseId) ? _moistureRepository.GetLatest(greenhouseId, potId) : null;
    }
}