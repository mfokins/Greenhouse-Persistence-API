

using Data.Models;

namespace Data.Mappers
{
    public class DomToDb
    {
        public static Models.Measurements.TemperatureMeasurement Convert(Core.Models.TemperatureMeasurement entity)
        {
            return new Models.Measurements.TemperatureMeasurement()
            {
                Temperature = entity.Temperature,
                Time = entity.Time
            };
        }


        public static Models.Greenhouse Convert(Core.Models.Greenhouse entity)
        {
            return new Models.Greenhouse() { GreenHouseId = entity.GreenHouseId };
        }

        public static Models.Measurements.DioxideCarbonMeasurement Convert(Core.Models.DioxideCarbonMeasurement entity)
        {
            return new Models.Measurements.DioxideCarbonMeasurement()
            {
                Co2Measurement = entity.Co2Measurement,
                Time = entity.Time
            };
        }

        internal static Models.Pot Convert(Core.Models.Pot entity)
        {
            return new Models.Pot
            {
                MoistureThreshold = Convert(entity.moistureThreshold),
                Name = entity.Name,
                Id = entity.Id,

            };
        }

        public static Threshold Convert(Core.Models.Threshold threshold)
        {
            return new Threshold
            {
                HigherThreshold = threshold.HigherThreshold,
                LowerThreshold = threshold.LowerThreshold,
                Type = (ThresholdType)threshold.Type
            };
        }

        public static Models.Measurements.HumidityMeasurement Convert(Core.Models.HumidityMeasurement entity)
        {
            return new Models.Measurements.HumidityMeasurement()
            {
                Humidity = entity.Humidity,
                Time = entity.Time

            };
        }
    }
}