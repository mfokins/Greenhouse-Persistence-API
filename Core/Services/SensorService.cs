
using Core.Interfaces.Sensors;
using Core.Models;

namespace Core.Services
{
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _sensorRepository;
        public SensorService(ISensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }
        public IList<SensorStatus> GetSensorStatuses(string greenhouseId)
        {
            List<SensorStatus> potStatuses = _sensorRepository.GetSensorStatusesPot(greenhouseId).ToList();
            List<SensorStatus> greenhouseStatuses = _sensorRepository.GetSensorStatusGreenhouse(greenhouseId).ToList();
            potStatuses.AddRange(greenhouseStatuses);
            return potStatuses;
        }
        
        public void SetSensorStatus(SensorStatus sensorStatus, string greenhouseId, int? sensorId)
        {
            if (sensorId == null)
            {
                _sensorRepository.SetSensorStatusGreenhouse(sensorStatus, greenhouseId);
            }
            else
            {
                _sensorRepository.SetSensorStatusPot(sensorStatus, sensorId.Value, greenhouseId);
            }
        }
    }
}
