using Core.Models;

namespace Core.Interfaces
{
    public interface IThresholdRepository
    {
        public Threshold GetTemperatureThresholds(string greenhouseId);
        public void SetTemperatureThresholds(string greenhouseId, Threshold threshold);
        public Threshold GetHumidityThresholds(string greenhouseId);
        public void SetHumidityThresholds(string greenhouseId, Threshold threshold);
        public Threshold GetDioxideCarbonThresholds(string greenhouseId);
        public void SetDioxideCarbonThresholds(string greenhouseId, Threshold threshold);
        public Threshold GetMoisturehresholds(string greenhouseId, int potId);
        public void SetMoistureThresholds(string greenhouseId, int potId, Threshold threshold);

    }
}
