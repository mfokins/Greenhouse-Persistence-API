using Core.Models;

namespace Core.Interfaces.Sensors
{
    public interface ISensorRepository
    {
        //sensorId is for if u want to set a status for a moisture sensor
        void SetSensorStatusGreenhouse(SensorStatus sensorStatus, string greenhouseId);
        void SetSensorStatusPot(SensorStatus sensorStatus, int sensorId, string greenhouseId);

        IList<SensorStatus> GetSensorStatusGreenhouse(string greenhouseId);
        IList<SensorStatus> GetSensorStatusesPot(string greenhouseId);

    }
}
