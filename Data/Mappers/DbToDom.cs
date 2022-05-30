﻿using Data.Models;

namespace Data.Mappers
{
    public class DbToDom
    {
        public static Core.Models.TemperatureMeasurement Convert(Models.Measurements.TemperatureMeasurement temperatureMeasurement)
        {
            return new Core.Models.TemperatureMeasurement()
            {
                Temperature = temperatureMeasurement.Temperature,
                Time = temperatureMeasurement.Time,
            };
        }

        public static Core.Models.DioxideCarbonMeasurement Convert(Models.Measurements.DioxideCarbonMeasurement dioxideCarbonMeasurement)
        {
            return new Core.Models.DioxideCarbonMeasurement()
            {
                Co2Measurement = dioxideCarbonMeasurement.Co2Measurement,
                Time = dioxideCarbonMeasurement.Time
            };
        }

        internal static Core.Models.SensorStatus Convert(SensorStatus? sensorstatuses)
        {
            throw new System.NotImplementedException();
            if (sensorstatuses == null)
            {
                //return SensorStatus.Empty;
            }
        }

        internal static Core.Models.Greenhouse Convert(Greenhouse greenhouse)
        {
            return new()
            {
                GreenHouseId = greenhouse.GreenHouseId,
            };
        }


        public static Core.Models.HumidityMeasurement Convert(Models.Measurements.HumidityMeasurement humidityMeasurement)
        {
            return new Core.Models.HumidityMeasurement()
            {
                Humidity = humidityMeasurement.Humidity,
                Time = humidityMeasurement.Time
            };
        }
        public static Core.Models.MoistureMeasurement Convert(Models.Measurements.MoistureMeasurement moistureMeasurement, int potId)
        {
            return new Core.Models.MoistureMeasurement()
            {
                Moisture = moistureMeasurement.Moisture,
                Time = moistureMeasurement.Time,
                PotId = potId
            };
        }

        internal static Core.Models.Pot Convert(Pot pot)
        {
            return new Core.Models.Pot
            {
                Id = pot.Id,
                moistureThreshold = Convert(pot.MoistureThreshold),
                Name = pot.Name,
                MoistureSensorId = pot.MoistureSensorId,
                
            };
        }
        
        public static Core.Models.Threshold Convert(Threshold threshold)
        {
            if (threshold == null)
                return Core.Models.Threshold.Empty;
            return new Core.Models.Threshold()
            {
                HigherThreshold = threshold.HigherThreshold,
                LowerThreshold = threshold.LowerThreshold,
                Type = (Core.Models.ThresholdType)threshold.Type
            };
        }
    }

}