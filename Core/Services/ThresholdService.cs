using Core.Interfaces;
using Core.Models;


namespace Core.Services
{
    public class ThresholdService : IThresholdService
    {
        private readonly IThresholdRepository _thresholdRepository;

        public ThresholdService(IThresholdRepository thresholdRepository)
        {
            _thresholdRepository = thresholdRepository;
        }
        public Threshold GetDioxideCarbonThresholds(string greenhouseId)
        {
            return _thresholdRepository.GetDioxideCarbonThresholds(greenhouseId);
        }

        public Threshold GetHumidityThresholds(string greenhouseId)
        {
            return _thresholdRepository.GetHumidityThresholds(greenhouseId);
        }

        public Threshold GetMoisturehresholds(string greenhouseId, int potId)
        {
            return _thresholdRepository.GetMoisturehresholds(greenhouseId, potId);
        }

        public Threshold GetTemperatureThresholds(string greenhouseId)
        {
            return _thresholdRepository.GetTemperatureThresholds(greenhouseId);
        }

        public void SetDioxideCarbonThresholds(string greenhouseId, Threshold threshold)
        {
            _thresholdRepository.SetDioxideCarbonThresholds(greenhouseId, threshold);
        }

        public void SetHumidityThresholds(string greenhouseId, Threshold threshold)
        {
            _thresholdRepository.SetHumidityThresholds(greenhouseId, threshold);
        }

        public void SetMoistureThresholds(string greenhouseId, int potId, Threshold threshold)
        {
            _thresholdRepository.SetMoistureThresholds(greenhouseId, potId, threshold);
        }

        public void SetTemperatureThresholds(string greenhouseId, Threshold threshold)
        {
            _thresholdRepository.SetTemperatureThresholds(greenhouseId, threshold);
        }
    }
}
