using Core.Interfaces;
using Core.Interfaces.Greenhouse;
using Core.Interfaces.Pot;
using Core.Models;
using Core.Services.Interfaces;

namespace Core.Services;

public class MoistureService : IMoistureService
{
    private readonly IMoistureRepository _moistureRepository;

    private readonly IGreenhouseService _greenhouseService;
    private readonly IThresholdService _thresholdService;
    private readonly INotificationService _notificationService;
    private readonly IPotService _potService;

    public MoistureService(IMoistureRepository moistureRepository, IGreenhouseService greenhouseService, IThresholdService thresholdService, INotificationService notificationService, IPotService potService)
    {
        _moistureRepository = moistureRepository;
        _greenhouseService = greenhouseService;
        _thresholdService = thresholdService;
        _notificationService = notificationService;
        _potService = potService;
    }

    public void Add(MoistureMeasurement entity)
    {
        if (!_greenhouseService.IsCreated(entity.GreenHouseId))
        {
            _greenhouseService.Create(entity.GreenHouseId);
        }


        var threshold = _thresholdService.GetMoisturehresholds(entity.GreenHouseId, entity.PotId);
        if (threshold.LowerThreshold > entity.Moisture)
        {
            //checks if notification wasnt already sent
            try
            {
                if (!(_moistureRepository.GetLatest(entity.GreenHouseId, entity.PotId).Moisture < threshold.LowerThreshold))
                {
                    var pot = _potService.Get(entity.PotId, entity.GreenHouseId);
                    _notificationService.SendMoistureThreshold(pot.Name, entity.GreenHouseId);
                }
            }
            catch (NullReferenceException exception)
            {
                //no previous mesurments
                var pot = _potService.Get(entity.PotId, entity.GreenHouseId);
                _notificationService.SendMoistureThreshold(pot.Name, entity.GreenHouseId);
            }
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