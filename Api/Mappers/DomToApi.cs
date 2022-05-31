
using Api.Models;
using TemperatureMeasurement = Core.Models.TemperatureMeasurement;
using HumidityMeasurement = Core.Models.HumidityMeasurement;
using DioxideCarbonMeasurement = Core.Models.DioxideCarbonMeasurement;
using MoistureMeasurement = Core.Models.MoistureMeasurement;

namespace Api.Mappers
{
    public class DomToApi
    {
        public static Models.TemperatureMeasurement Convert(TemperatureMeasurement temperatureMeasurement)
        {
            return new Api.Models.TemperatureMeasurement
            {
                Temperature = temperatureMeasurement.Temperature,
                Time = ((DateTimeOffset)temperatureMeasurement.Time).ToUnixTimeSeconds()
            };
        }

        public static SensorStatus Convert(Core.Models.SensorStatus status)
        {
            return new SensorStatus
            {
                Sensor = status.Name == "" ? status.Type.ToString() : status.Name,
                Status = status.IsWorking
            };
        }

        public static Models.HumidityMeasurement Convert(HumidityMeasurement humidityMeasurement)
        {
            return new Api.Models.HumidityMeasurement()
            {
                Humidity = Math.Round(humidityMeasurement.Humidity, 2),
                Time = ((DateTimeOffset)humidityMeasurement.Time).ToUnixTimeSeconds()
            };
        }
        public static Models.DioxideCarbonMeasurement Convert(DioxideCarbonMeasurement dioxideCarbonMeasurement)
        {
            return new Api.Models.DioxideCarbonMeasurement()
            {
                Co2Measurement = dioxideCarbonMeasurement.Co2Measurement,
                Time = ((DateTimeOffset)dioxideCarbonMeasurement.Time).ToUnixTimeSeconds()
            };
        }
        public static Models.MoistureMeasurement Convert(MoistureMeasurement moistureMeasurement)
        {
            return new Api.Models.MoistureMeasurement()
            {
                Moisture = moistureMeasurement.Moisture,
                PotId = moistureMeasurement.PotId,
                Time = ((DateTimeOffset)moistureMeasurement.Time).ToUnixTimeSeconds()
            };
        }

        public static Models.Threshold Convert(Core.Models.Threshold threshold)
        {
            return new Api.Models.Threshold()
            {
                UpperThreshold = threshold.HigherThreshold,
                LowerThreshold = threshold.LowerThreshold
            };
        }


        public static Models.Pot Convert(Core.Models.Pot pot)
        {

            return new Models.Pot()
            {
                LowerMoistureThreshold = pot.moistureThreshold.LowerThreshold,
                Name = pot.Name,
                Id = pot.Id,
                MoistureSensorId = pot.MoistureSensorId,

            };
        }
    }
}