using Api.Models;
using Core.Models;

using DioxideCarbonMeasurement = Core.Models.DioxideCarbonMeasurement;
using TemperatureMeasurement = Core.Models.TemperatureMeasurement;
using HumidityMeasurement = Core.Models.HumidityMeasurement;
using MoistureMeasurement = Core.Models.MoistureMeasurement;
using Pot = Core.Models.Pot;

namespace Api.Mappers
{
    public class ApiToDom
    {
        public static TemperatureMeasurement Convert(Models.TemperatureMeasurement value)
        {
            return new TemperatureMeasurement
            {
                Temperature = value.Temperature,
                Time = UnixTimeStampToDateTime(value.Time),
            };
        }


        public static HumidityMeasurement Convert(Models.HumidityMeasurement value)
        {
            return new HumidityMeasurement()
            {
                Humidity = value.Humidity,
                Time = UnixTimeStampToDateTime(value.Time),
            };
        }

        public static DioxideCarbonMeasurement Convert(Models.DioxideCarbonMeasurement value)
        {
            return new DioxideCarbonMeasurement()
            {
                Co2Measurement = value.Co2Measurement,
                Time = UnixTimeStampToDateTime(value.Time),
            };
        }

        public static MoistureMeasurement Convert(Models.MoistureMeasurement value)
        {
            return new MoistureMeasurement()
            {
                Moisture = value.Moisture,
                PotId = value.PotId,
                Time = UnixTimeStampToDateTime(value.Time),
            };
        }

        public static Pot Convert(Models.Pot pot)
        {
            return new Pot()
            {
                moistureThreshold = new Core.Models.Threshold()
                {
                    Type = ThresholdType.Moisture,
                    LowerThreshold = pot.LowerMoistureThreshold
                },
                Name = pot.Name,
                Id = pot.Id,
                MoistureSensorId = pot.MoistureSensorId,
            };
        }

        public static Core.Models.Threshold Convert(Models.Threshold threshold)
        {
            return new Core.Models.Threshold()
            {
                LowerThreshold = threshold.LowerThreshold,
                HigherThreshold = threshold.UpperThreshold,
            };
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}