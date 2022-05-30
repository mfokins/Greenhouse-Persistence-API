using Core.Models;

namespace Core.Interfaces.Sensors
{
    public interface ISensorService
    {
        //sensorId is for if u want to set a status for a moisture sensor
        void SetSensorStatus(SensorStatus sensorStatus, string greenhouseId, int? sensorId);
        IList<SensorStatus> GetSensorStatuses(string greenhouseId);
    }
}
